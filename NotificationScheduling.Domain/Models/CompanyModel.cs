using System;
using System.ComponentModel.DataAnnotations;

namespace NotificationScheduling.Domain.Models
{
    public class CompanyModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CompanyNumber { get; set; }

        [Required]
        public string CompanyType { get; set; }

        [Required]
        public string Market { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}
