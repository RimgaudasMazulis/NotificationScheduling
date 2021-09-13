using System;
using System.Collections.Generic;

namespace NotificationScheduling.Domain.Models
{
    public class CompanyNotificationsModel
    {
        public Guid CompanyId { get; set; }
        public IEnumerable<string> Notifications { get; set; }
    }
}
