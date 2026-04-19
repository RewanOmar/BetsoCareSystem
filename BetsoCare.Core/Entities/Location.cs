using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Location
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Hours { get; set; }
        public string? Note { get; set; }
        public string? Services { get; set; }

        public string? Type { get; set; }
        public bool? IsInquiryOnly { get; set; }
    }
}
