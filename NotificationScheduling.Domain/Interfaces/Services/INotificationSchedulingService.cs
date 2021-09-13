using System.Collections.Generic;
using NotificationScheduling.Domain.Models;
using System.Threading.Tasks;

namespace NotificationScheduling.Domain.Interfaces.Services
{
    public interface INotificationSchedulingService
    {
        Task<CompanyNotificationsModel> CreateScheduleWithCompanyData(CompanyModel companyModel);
        Task<IEnumerable<CompanyNotificationsModel>> GetAllCompanySchedules();
    }
}
