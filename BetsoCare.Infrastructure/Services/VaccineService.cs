using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Infrastructure.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _repo;

        public VaccineService(IVaccineRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateAsync(string userId, CreateVaccineDto dto)
        {
            var vaccine = new Vaccine
            {
                UserId = userId,
                Pet = dto?.Pet,
                Name = dto?.Name,
                Date = dto?.Date,
                Reminder = dto?.Reminder ?? false,
                Completed = false
            };

            await _repo.AddAsync(vaccine);
            await _repo.SaveChangesAsync();
        }

        public async Task CompleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return;

            var vaccine = await _repo.GetById(id);
            if (vaccine == null) return;

            vaccine.Completed = true;

            await _repo.SaveChangesAsync();
        }

        public async Task<List<Vaccine>> GetUserVaccines(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<Vaccine>();

            return await _repo.GetByUser(userId);
        }

       
    }
}
