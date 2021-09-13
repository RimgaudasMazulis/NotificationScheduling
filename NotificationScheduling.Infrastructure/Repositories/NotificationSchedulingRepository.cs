using Microsoft.EntityFrameworkCore;
using NotificationScheduling.Domain.Entities;
using NotificationScheduling.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotificationScheduling.Infrastructure.Repositories
{
    public static class NotificationSchedulingRepository
    {
        public static async Task<IEnumerable<Company>> GetAllCompanySchedules(this IRepository<Company> repository)
        {
            return await repository.Entities.Include("Schedule").ToListAsync();
        }
    }
}
