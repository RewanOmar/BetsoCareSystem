using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class DangerousAnimalDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }

        public string Governorate { get; set; }
        public string District { get; set; }

        public string AnimalType { get; set; }

        public DateTime ReportDate { get; set; }

        public string LocationCity { get; set; }

        public List<string> SelectedSymptoms { get; set; }

        public string? OtherSymptom { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
