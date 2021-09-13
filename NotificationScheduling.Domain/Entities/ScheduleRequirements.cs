using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationScheduling.Domain.Entities
{
    public class ScheduleRequirements
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Market")] 
        public int MarketId { get; set; }

        public virtual Market Market { get; set; }

        public int NotificationsCount { get; set; }
        public string SendOnDays { get; set; }
        public string AllowedCompanyTypes { get; set; }
    }
}