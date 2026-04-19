using BetsoCare.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface IShelterRepository
    {
        Task<List<Shelter>> GetAllAsync();
        Task<Shelter?> GetByIdAsync(int id);
        Task AddAsync(Shelter shelter);
        Task UpdateAsync(Shelter shelter);
        Task DeleteAsync(int id);
    }
}
