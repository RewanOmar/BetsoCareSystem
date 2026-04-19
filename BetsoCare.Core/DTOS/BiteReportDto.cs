using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class BiteReportDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }

        public string Governorate { get; set; }
        public string District { get; set; }

        public string AnimalType { get; set; }
        public string ExposureType { get; set; }
        public string Severity { get; set; }

        public DateTime ExposureDateTime { get; set; }

        public string? LocationCity { get; set; }

        public List<string> BodyLocations { get; set; }
        public List<string> InitialActions { get; set; }

        public string? OtherBodyLocation { get; set; }
        public string? OtherAction { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
