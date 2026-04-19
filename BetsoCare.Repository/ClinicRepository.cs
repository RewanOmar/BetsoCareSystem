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
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clinic>> GetAllAsync()
        {
            return await _context.Clinics
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Clinic?> GetByIdAsync(int id)
        {
            return await _context.Clinics
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Clinic> AddAsync(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            await _context.SaveChangesAsync();
            return clinic;
        }

        public async Task<Clinic?> UpdateAsync(Clinic clinic)
        {
            var existing = await _context.Clinics.FindAsync(clinic.Id);

            if (existing == null)
                return null;

            existing.Name = clinic.Name;
            existing.Address = clinic.Address;
            existing.Governorate = clinic.Governorate;
            existing.Phone = clinic.Phone;
            existing.FacebookPage = clinic.FacebookPage;
            existing.ImageUrl = clinic.ImageUrl;
            existing.Latitude = clinic.Latitude;
            existing.Longitude = clinic.Longitude;
            existing.BookingPrice = clinic.BookingPrice;
            existing.WorkingDays = clinic.WorkingDays;
            existing.WorkingHours = clinic.WorkingHours;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var clinic = await _context.Clinics.FindAsync(id);

            if (clinic == null)
                return false;

            _context.Clinics.Remove(clinic);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}