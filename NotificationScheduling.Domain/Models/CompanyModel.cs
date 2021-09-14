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
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid company number")]
        public string CompanyNumber { get; set; }

        [Required]
        public string CompanyType { get; set; }

        [Required]
        public string Market { get; set; }
    }
}
