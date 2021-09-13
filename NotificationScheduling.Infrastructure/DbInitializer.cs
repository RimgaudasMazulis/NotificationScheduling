using NotificationScheduling.Domain.Entities;
using System;
using System.Linq;

namespace NotificationScheduling.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(NotificationSchedulingContext context)
        {
            context.Database.EnsureCreated();

            if (context.CompanyTypes.Any())
            {
                return;   // Db is created
            }

            var smallCompany = new CompanyType() { Name = "small" };
            var mediumCompany = new CompanyType() { Name = "medium" };
            var largeCompany = new CompanyType() { Name = "large" };

            var companyTypes = new CompanyType[] { smallCompany, mediumCompany, mediumCompany, largeCompany };

            context.CompanyTypes.AddRange(companyTypes);

            var denmarkMarket = new Market() { Name = "Denmark" };
            var norwayMarket = new Market() { Name = "Norway" };
            var swedenMarket = new Market() { Name = "Sweden" };
            var finlandMarket = new Market() { Name = "Finland" };

            context.Markets.AddRange(new Market[] { denmarkMarket, norwayMarket, swedenMarket, finlandMarket });

            var scheduleRequirements = new ScheduleRequirements[]
            {
                new ScheduleRequirements(){ Market = denmarkMarket, NotificationsCount = 5, SendOnDays = "1,5,10,15,20", AllowedCompanyTypes = "1,2,3"},
                new ScheduleRequirements(){ Market = norwayMarket, NotificationsCount = 4, SendOnDays = "1,5,10,20", AllowedCompanyTypes = "1,2,3"},
                new ScheduleRequirements(){ Market = swedenMarket, NotificationsCount = 4, SendOnDays = "1,7,14,28", AllowedCompanyTypes = "1,2"},
                new ScheduleRequirements(){ Market = finlandMarket, NotificationsCount = 5, SendOnDays = "1,5,10,15,20", AllowedCompanyTypes = "3"},
            };

            context.ScheduleRequirements.AddRange(scheduleRequirements);

            context.SaveChanges();

            // Examples

            var companies = new Company[]
            {
                new Company(){ Name = "ExampleCompany", CompanyType = smallCompany, Market = denmarkMarket, CompanyNumber = "0123456789", Id = new Guid("aad7a630-af1c-4952-9cb4-44b8b847853b"), CreatedAt = DateTime.Now }
            };

            context.Companies.AddRange(companies);

            context.SaveChanges();
        }
    }
}
