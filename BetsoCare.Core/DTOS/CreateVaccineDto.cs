using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class CreateVaccineDto
    {
        public string? Pet { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
        public bool? Reminder { get; set; }
    }
}
