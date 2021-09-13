using Microsoft.EntityFrameworkCore;
using NotificationScheduling.Domain.Entities;

namespace NotificationScheduling.Infrastructure
{
    public class NotificationSchedulingContext : DbContext
    {
        public NotificationSchedulingContext(DbContextOptions<NotificationSchedulingContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<ScheduleRequirements> ScheduleRequirements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<CompanyType>().ToTable("CompanyTypes");
            modelBuilder.Entity<Market>().ToTable("Markets");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
            modelBuilder.Entity<ScheduleRequirements>().ToTable("ScheduleRequirements");
        }
    }
}
