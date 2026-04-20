using BetsoCare.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface IClinicRepository
    {
        Task<IEnumerable<Clinic>> GetAllAsync();

        Task<Clinic?> GetByIdAsync(int id);

        Task<Clinic> AddAsync(Clinic clinic);

        Task<Clinic?> UpdateAsync(Clinic clinic);

        Task<bool> DeleteAsync(int id);
    }
}
