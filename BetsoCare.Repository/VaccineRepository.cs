using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Repository
{
    public class VaccineRepository : IVaccineRepository
    {
        private readonly ApplicationDbContext _context;

        public VaccineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vaccine vaccine)
        {
            await _context.Vaccines.AddAsync(vaccine);
        }

       

        public async Task<Vaccine?> GetById(string id)
        {
            return await _context.Vaccines.FindAsync(id);
        }

        public async Task<List<Vaccine>> GetByUser(string userId)
        {
            return await _context.Vaccines
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
        
    }
}
