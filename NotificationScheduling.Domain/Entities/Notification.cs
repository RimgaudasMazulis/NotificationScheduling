using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationScheduling.Domain.Entities
{
    public class Notification
    {
        [Key] public int Id { get; set; }

        public DateTime SendingDate { get; set; }

        [ForeignKey("Schedule")] 
        public int ScheduleId { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}