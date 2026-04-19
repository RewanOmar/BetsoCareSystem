using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Infrastructure.Services
{
    public class StatsService : IStatsService
    {
        private readonly ApplicationDbContext _context;

        public StatsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> TotalCompleted()
        {
            return await _context.Vaccines.CountAsync(x => x.Completed == true);
        }

        public async Task<int> TotalVaccines()
        {
            return await _context.Vaccines.CountAsync();
        }
    }
}
