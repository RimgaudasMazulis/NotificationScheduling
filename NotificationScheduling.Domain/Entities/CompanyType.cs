using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NotificationScheduling.Domain.Entities
{
    public class CompanyType
    {
        [Key] public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}