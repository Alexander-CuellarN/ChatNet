using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;

namespace Services.Services
{
    public class SalaService : SalaRepository<Sala>
    {
        private ChatContext _context;

        public SalaService(ChatContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateSala(Sala sala)
        {
            try
            {
                _context.Salas.Add(sala);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteSala(Sala sala)
        {
            try
            {
                _context.Salas.Remove(sala);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Sala> FindSalaById(int id)
        {
            var sala = await _context.Salas.FindAsync(id);
            return sala;
        }

        public async Task<List<Sala>> ListSalas()
        {
            return await _context.Salas.ToListAsync();

        }
        public async Task<bool> UpdateSala(Sala sala)
        {
            try
            {
                _context.Salas.Entry(sala).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
