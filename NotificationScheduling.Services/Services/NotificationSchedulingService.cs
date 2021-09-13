using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NotificationScheduling.Domain.Cache;
using NotificationScheduling.Domain.Entities;
using NotificationScheduling.Domain.Exceptions;
using NotificationScheduling.Domain.Interfaces;
using NotificationScheduling.Domain.Interfaces.Services;
using NotificationScheduling.Domain.Models;
using NotificationScheduling.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationScheduling.Infrastructure.Repositories;

namespace NotificationScheduling.Services.Services
{
    public class NotificationSchedulingService : INotificationSchedulingService
    {
        private readonly ILogger<NotificationSchedulingService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private IMemoryCache _cache;

        public NotificationSchedulingService(ILogger<NotificationSchedulingService> logger, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<CompanyNotificationsModel> CreateScheduleWithCompanyData(CompanyModel companyModel)
        {
            try
            {
                var companyEntity = await _unitOfWork.Repository<Company>().FindAsync(companyModel.Id);

                if (companyEntity != null)
                {
                    throw new DuplicateEntryException($"Error : company entity with the same id = {companyModel.Id}, is already in database.");
                }

                await _unitOfWork.BeginTransaction();

                companyEntity = await CreateCompanyEntity(companyModel);
                await _unitOfWork.Repository<Company>().InsertAsync(companyEntity);

                var scheduleEntity = await CreateScheduleEntity(companyEntity);
                await _unitOfWork.Repository<Schedule>().InsertAsync(scheduleEntity);

                await _unitOfWork.CommitTransaction();

                InvalidateCache(CacheConstants.SchedulesCacheName);

                return CreateCompanyNotificationModel(companyEntity);
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
        public async Task<IEnumerable<CompanyNotificationsModel>> GetAllCompanySchedules()
        {
            if (_cache.TryGetValue(CacheConstants.SchedulesCacheName, out IEnumerable<Company> value))
            {
                return value.Select(CreateCompanyNotificationModel);
            }

            var companySchedules = await _unitOfWork.Repository<Company>().GetAllCompanySchedules();
            _cache.Set(CacheConstants.SchedulesCacheName, companySchedules, DateTimeOffset.Now.AddMinutes(AppSettings.CacheTimeoutInMinutes));

            return companySchedules.Select(CreateCompanyNotificationModel);
        }

        private CompanyNotificationsModel CreateCompanyNotificationModel(Company companyEntity)
        {
            var result = new CompanyNotificationsModel()
            {
                CompanyId = companyEntity.Id,
            };

            if (companyEntity.Schedule != null && companyEntity.Schedule.Notifications != null)
            {
                result.Notifications = companyEntity.Schedule.Notifications.Select(o => o.SendingDate.FormatDate()).ToArray();
            }

            return result;
        }

        private async Task<Schedule> CreateScheduleEntity(Company companyEntity)
        {
            var scheduleRequirements = await GetAllScheduleRequirementsFromCache();

            var scheduleRequirement = scheduleRequirements.FirstOrDefault(sr => sr.MarketId == companyEntity.MarketId);

            var allowedCompanyTypes = scheduleRequirement?.AllowedCompanyTypes.CommaSeparatedToNumbers();

            if (allowedCompanyTypes == null || !allowedCompanyTypes.Contains(companyEntity.CompanyTypeId))
            {
                return null;
            }

            var notifications = CreateScheduleNotifications(companyEntity, scheduleRequirement);

            var schedule = new Schedule()
            {                
                Company = companyEntity,
                Notifications = notifications
            };

            return schedule;
        }

        private List<Notification> CreateScheduleNotifications(Company companyEntity, ScheduleRequirements scheduleRequirement)
        {
            var notifications = new List<Notification>();

            var sentOnDays = scheduleRequirement.SendOnDays.CommaSeparatedToNumbers();

            foreach (var days in sentOnDays)
            {
                notifications.Add(
                    new Notification()
                    {
                        SendingDate = companyEntity.CreatedAt.AddDays(days)
                    }
                );
            }

            return notifications;
        }

        private async Task<Company> CreateCompanyEntity(CompanyModel model)
        {
            var companyTypes = await GetAllCompanyTypesFromCache();

            var companyType = companyTypes.FirstOrDefault(ct => ct.Name.Equals(model.CompanyType, StringComparison.InvariantCultureIgnoreCase));

            if (companyType == null)
            {
                return null;
            }

            var markets = await GetAllMarketsFromCache();

            var market = markets.FirstOrDefault(ct => ct.Name.Equals(model.Market, StringComparison.InvariantCultureIgnoreCase));

            if (market == null)
            {
                return null;
            }

            var entity = new Company()
            {
                Id = model.Id,
                Name = model.Name,
                CompanyNumber = model.CompanyNumber,
                CompanyType = companyType,
                Market = market,              
                CreatedAt = DateTime.Now
            };

            return entity;
        }

        private async Task<IEnumerable<CompanyType>> GetAllCompanyTypesFromCache()
        {
            if (_cache.TryGetValue(CacheConstants.CompanyTypeCacheName, out IEnumerable<CompanyType> value))
            {
                return value;
            }

            var companyTypes = await _unitOfWork.Repository<CompanyType>().GetAllAsync();
            _cache.Set(CacheConstants.CompanyTypeCacheName, companyTypes, DateTimeOffset.Now.AddMinutes(AppSettings.CacheTimeoutInMinutes));

            return companyTypes;
        }

        private async Task<IEnumerable<Market>> GetAllMarketsFromCache()
        {
            if (_cache.TryGetValue(CacheConstants.MarketsCacheName, out IEnumerable<Market> value))
            {
                return value;
            }

            var markets =  await _unitOfWork.Repository<Market>().GetAllAsync();
            _cache.Set(CacheConstants.CompanyTypeCacheName, markets, DateTimeOffset.Now.AddMinutes(AppSettings.CacheTimeoutInMinutes));

            return markets;
        }

        private async Task<IEnumerable<ScheduleRequirements>> GetAllScheduleRequirementsFromCache()
        {
            if (_cache.TryGetValue(CacheConstants.ScheduleRequirementsCacheName, out IEnumerable<ScheduleRequirements> value))
            {
                return value;
            }

            var scheduleRequirements = await _unitOfWork.Repository<ScheduleRequirements>().GetAllAsync();
            _cache.Set(CacheConstants.ScheduleRequirementsCacheName, scheduleRequirements, DateTimeOffset.Now.AddMinutes(AppSettings.CacheTimeoutInMinutes));

            return scheduleRequirements;
        }

        private void InvalidateCache(string key)
        {
            _cache.Remove(key);
        }
    }
}
