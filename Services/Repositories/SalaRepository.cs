using Data.Models;

namespace Services.Repositories
{
    public interface SalaRepository<T> where T : class
    {
        Task<List<Sala>> ListSalas();
        Task<bool> CreateSala(T sala);
        Task<bool> UpdateSala(T sala);
        Task<bool> DeleteSala(T sala);

        Task<T> FindSalaById(int id);
    }
}
