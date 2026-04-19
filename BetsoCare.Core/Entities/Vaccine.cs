using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Entities
{
    public class Vaccine
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string? UserId { get; set; }

        public string? Pet { get; set; }
        public string? Name { get; set; }

        public DateTime? Date { get; set; }

        public bool? Reminder { get; set; }
        public bool? Completed { get; set; }
    }
}
