using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Shelter
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Governorate { get; set; }
        public string? Address { get; set; }

        public string? AnimalType { get; set; }
        public string? Capacity { get; set; }

        public string? Phone { get; set; }
        public string? WorkingHours { get; set; }
        public string? ProfileImageUrl { get; set; }

        public string? Notes { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
