using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Clinic
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Governorate { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? FacebookPage { get; set; }

        public string? ImageUrl { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public decimal BookingPrice { get; set; }

        public string WorkingDays { get; set; } = null!;

        public string WorkingHours { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
