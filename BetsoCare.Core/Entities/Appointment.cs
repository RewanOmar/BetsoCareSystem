using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public decimal Price { get; set; }
        public string Status { get; set; } = "Pending";

        public string? RejectReason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
