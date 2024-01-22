using Data;
using Data.Models;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class MensajeService : mensajeRepository<Mensaje>
    {
        public ChatContext _context;

        public MensajeService(ChatContext context)
        {
            _context = context;
        }

        public async Task<Mensaje> CreateMessages(Mensaje message)
        {
            try
            {
                _context.Mensajes.Add(message);
                await _context.SaveChangesAsync();
                return message;
            }
            catch (Exception)
            {
                return new Mensaje();
            }
        }

        public async Task<bool> DeleteMessage(int idMessage)
        {
            try
            {
                var message = _context.Mensajes.Find(idMessage);
                if (message == null) return false;
                _context.Mensajes.Remove(message);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ICollection<Mensaje>> GetMessageByRoom(int roomId)
        {
            var messagesList = new List<Mensaje>();
            try
            {
                messagesList = await _context.Mensajes
                    .Where(m => m.SalaId == roomId)
                    .Include(u => u.UsuarioNavigation)
                    .ToListAsync();

                return messagesList;
            }
            catch (Exception)
            {
                return messagesList;
            }
        }

        public async Task DeleteMessageofUser(int id)
        {
            var messages = _context.Mensajes.Where(m => m.UsuarioId == id).ToArray();
            _context.Mensajes.RemoveRange(messages);
            await _context.SaveChangesAsync();
        }
    }
}
