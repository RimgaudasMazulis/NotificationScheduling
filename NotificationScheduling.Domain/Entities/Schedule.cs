using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationScheduling.Domain.Entities
{
    public class Schedule
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Company")] 
        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}