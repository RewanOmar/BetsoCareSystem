using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class CreateAppointmentDto
    {
        public int ClinicId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }
    }
}
