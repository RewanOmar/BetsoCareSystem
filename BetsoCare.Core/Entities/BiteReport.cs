using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class BiteReport
    {
        public int Id { get; set; }

        public string AnimalType { get; set; }
        public string ExposureType { get; set; }
        public string Severity { get; set; }

        public DateTime ExposureDateTime { get; set; }

        public string? LocationCity { get; set; }

        public string BodyLocations { get; set; }
        public string InitialActions { get; set; }
        public string? Symptoms { get; set; }
        public string? OtherBodyLocation { get; set; }
        public string? OtherAction { get; set; }

        public int ReportId { get; set; }
    }
}
