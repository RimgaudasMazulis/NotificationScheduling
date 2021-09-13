using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotificationScheduling.Domain.Interfaces;
using NotificationScheduling.Domain.Interfaces.Services;
using NotificationScheduling.Domain.Models;
using NotificationScheduling.Infrastructure;
using NotificationScheduling.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationScheduling.Tests
{
    [TestClass]
    public class NotificationSchedulingTests
    {
        private ILogger<NotificationSchedulingService> _logger;
        private IUnitOfWork _unitOfWork;
        private IMemoryCache _cache;
        private NotificationSchedulingService _service;
        private NotificationSchedulingContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddLogging();

            var connectionString = "Server=.;Database=NotificationScheduling;Trusted_Connection=True;MultipleActiveResultSets=True;";
            services.AddDbContext<NotificationSchedulingContext>(options =>
                {
                    options.UseSqlServer(connectionString,sqlOptions => sqlOptions.CommandTimeout(120));
                    options.UseLazyLoadingProxies();
                }
            );

            services.AddScoped<Func<NotificationSchedulingContext>>((provider) => () => provider.GetService<NotificationSchedulingContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationSchedulingService, NotificationSchedulingService>();

            var serviceProvider = services.BuildServiceProvider();

            _cache = serviceProvider.GetService<IMemoryCache>();
            var logFactory = serviceProvider.GetService<ILoggerFactory>();
            _logger = logFactory.CreateLogger<NotificationSchedulingService>();
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _context = serviceProvider.GetService<NotificationSchedulingContext>();

            _service = new NotificationSchedulingService(_logger, _unitOfWork, _cache);

            DbInitializer.Initialize(_context);
        }

        [TestMethod]
        public async Task ShouldAddCompany_ReturnScheduleNotifications()
        {
            var companyModel = GetCompanyModelData();

            var result = await _service.CreateScheduleWithCompanyData(companyModel);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CompanyNotificationsModel));
            Assert.IsTrue(result.Notifications.Any());
        }

        [TestMethod]
        public async Task ShouldGetCompanySchedules_ReturnScheduleNotifications()
        {
            var companyModel = GetCompanyModelData();

            var result = await _service.GetAllCompanySchedules();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CompanyNotificationsModel>));
            Assert.IsTrue(result.Any());
        }


        private CompanyModel GetCompanyModelData()
        {
            return new CompanyModel()
            {
                Id = new Guid(),
                Name = "Test",
                CompanyNumber = "554466882",
                CompanyType = "medium",
                Market = "Sweden",
                CreatedAt = DateTime.Now
            };
        }
    }
}
