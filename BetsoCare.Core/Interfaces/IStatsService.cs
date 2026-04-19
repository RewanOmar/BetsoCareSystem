using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface IStatsService
    {
        Task<int> TotalCompleted();
        Task<int> TotalVaccines();
    }
}
