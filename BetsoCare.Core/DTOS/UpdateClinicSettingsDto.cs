using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class UpdateClinicSettingsDto
    {
        public decimal BookingPrice { get; set; }

        public string WorkingDays { get; set; } = null!;

        public string WorkingHours { get; set; } = null!;
    }
}
