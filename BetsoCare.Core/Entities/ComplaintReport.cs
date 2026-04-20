using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class ComplaintReport
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public string Urgency { get; set; }

        public int ReportId { get; set; }
    }
}
