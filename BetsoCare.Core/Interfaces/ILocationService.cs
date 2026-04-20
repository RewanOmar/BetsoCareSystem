using BetsoCare.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetsoCare.Core.Interfaces
{
    public interface ILocationService
    {
        Task<List<Location>> GetAll(string type);
        Task Add(Location location);
        Task Delete(int id);
    }
}
