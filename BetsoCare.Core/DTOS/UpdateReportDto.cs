using BetsoCare.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.DTOS
{
    public class UpdateReportDto
    {
        public ReportStatus Status { get; set; }
        public string? AdminResponse { get; set; }
    }
}
