using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Mensaje> Mensajes { get; set; }
    }
}
