using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;

namespace Services.Services
{
    public class UsuarioService : UsuarioRepository<Usuario>
    {
        private readonly ChatContext _context;

        public UsuarioService(ChatContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUSuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUsuario(int idUsuario)
        {
            try
            {
                var usuario = await FindUserById(idUsuario);

                if (usuario == null)
                    return false;

                _context.Usuarios.Remove(usuario);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Usuario> FindAUser(string nickName)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.NickName == nickName)
                .FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Usuario> FindUserById(int usuarioId)
        {
            var usuario = await _context.Usuarios
                .FindAsync(usuarioId);
            return usuario;
        }

        public async Task<List<String>> ListUserByRoom(int RoomFilter)
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.Mensajes)
                .ThenInclude(m => m.salaNavigation)
                .Where(u => u.Mensajes.Any(m => m.SalaId == RoomFilter))
                .Select(u => u.NickName)
                .ToListAsync();

            return usuarios;
        }

        public async Task<bool> UdpateUsuario(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Entry(usuario).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
