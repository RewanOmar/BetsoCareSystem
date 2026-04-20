using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class DangerousAnimalReport
    {
        public int Id { get; set; }

        public string AnimalType { get; set; }

        public DateTime ReportDate { get; set; }

        public string LocationCity { get; set; }

        public string SelectedSymptoms { get; set; }

        public string? OtherSymptom { get; set; }

        public int ReportId { get; set; }
    }
}