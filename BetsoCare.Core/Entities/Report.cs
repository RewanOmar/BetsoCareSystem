using BetsoCare.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Report
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Phone { get; set; }

        public string? Governorate { get; set; }
        public string? District { get; set; }

        public ReportType Type { get; set; }

        public ReportStatus Status { get; set; } = ReportStatus.Pending;
        

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AdminResponse { get; set; }

        public BiteReport? BiteReport { get; set; }
        public DangerousAnimalReport? DangerousAnimalReport { get; set; }
        public ComplaintReport? ComplaintReport { get; set; }
        public string? UserId { get; set; }
    }
}
