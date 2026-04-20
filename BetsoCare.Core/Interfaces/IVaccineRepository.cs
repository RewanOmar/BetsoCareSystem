using BetsoCare.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface IVaccineRepository
    {
        Task AddAsync(Vaccine vaccine);
        Task<Vaccine?> GetById(string id);
        Task<List<Vaccine>> GetByUser(string userId);
        Task SaveChangesAsync();
    }
}
