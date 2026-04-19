using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class ComplaintDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }

        public string Urgency { get; set; }
        public string Governorate { get; set; }
        public string District { get; set; }
    }
}
