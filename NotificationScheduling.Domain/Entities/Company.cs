using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationScheduling.Domain.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyNumber { get; set; }

        [ForeignKey("CompanyType")]
        public int CompanyTypeId { get; set; }
        public virtual CompanyType CompanyType { get; set; }

        [ForeignKey("Market")]
        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}
