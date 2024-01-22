using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public interface SalaRepository<T> where T:class
    {
        Task<List<Sala>> ListSalas();
        Task<bool> CreateSala(T sala);
        Task<bool> UpdateSala(T sala);
        Task<bool> DeleteSala(T sala);

        Task<T> FindSalaById(int id);
    }
}
