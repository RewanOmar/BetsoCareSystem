using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BetsoCare.Core.DTOS
{
    public class CreateClinicDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Governorate { get; set; } = null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Url]
        public string? FacebookPage { get; set; }

        public IFormFile? Image { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Range(0, 10000)]
        public decimal BookingPrice { get; set; }

        [Required]
        public string WorkingDays { get; set; } = null!;

        [Required]
        public string WorkingHours { get; set; } = null!;

        public IFormFile? ImageUrl { get; set; }
    }

   
}
   