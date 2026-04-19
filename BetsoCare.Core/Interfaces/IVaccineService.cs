using BetsoCare.Core.DTOS;
using BetsoCare.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface IVaccineService
    {
        Task CreateAsync(string userId, CreateVaccineDto dto);
        Task CompleteAsync(string id);
        Task<List<Vaccine>> GetUserVaccines(string userId);
    }
}
