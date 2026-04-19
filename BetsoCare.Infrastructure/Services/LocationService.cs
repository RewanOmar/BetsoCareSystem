using BetsoCare.Core.Entities;
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
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAll(string type)
        {
            return await _context.Locations
                .Where(x => x.Type == type || type == null)
                .ToListAsync();
        }

        public async Task Add(Location location)
        {
            if (location == null) return;

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var loc = await _context.Locations.FindAsync(id);
            if (loc == null) return;

            _context.Locations.Remove(loc);
            await _context.SaveChangesAsync();
        }
    }
}
