using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Repository
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly ApplicationDbContext _context;

        public ShelterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shelter>> GetAllAsync()
            => await _context.Shelters.ToListAsync();

        public async Task<Shelter?> GetByIdAsync(int id)
            => await _context.Shelters.FindAsync(id);

        public async Task AddAsync(Shelter shelter)
        {
            _context.Shelters.Add(shelter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shelter shelter)
        {
            _context.Shelters.Update(shelter);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter != null)
            {
                _context.Shelters.Remove(shelter);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<User> UpdateProfile(int userId, UpdateProfileDto dto, IFormFile? image)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.Phone = dto.PhoneNumber;

            if (dto.DateOfBirth.HasValue)
                user.DateOfBirth = dto.DateOfBirth;

            if (!string.IsNullOrEmpty(dto.Address))
                user.Address = dto.Address;

            // 🔥 الجزء الجديد (الصورة)
            if (image != null && image.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                user.ProfileImageUrl = "/images/" + fileName;
            }

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
