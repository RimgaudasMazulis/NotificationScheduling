using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationScheduling.Domain.Interfaces;
using NotificationScheduling.Domain.Interfaces.Services;
using NotificationScheduling.Domain.Models;
using NotificationScheduling.Infrastructure;
using NotificationScheduling.Services.Services;
using System;


namespace NotificationScheduling.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add needed instances for database
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<NotificationSchedulingContext>(options =>
            {
                options.UseSqlServer(AppSettings.ConnectionString,
                    sqlOptions => sqlOptions.CommandTimeout(120));
                options.UseLazyLoadingProxies();
            }
            );

            services.AddScoped<Func<NotificationSchedulingContext>>((provider) => () => provider.GetService<NotificationSchedulingContext>());
            services.AddScoped<DbFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        /// <summary>
        /// Add instances of in-use services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<INotificationSchedulingService, NotificationSchedulingService>();
        }
    }
}
