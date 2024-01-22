using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public interface mensajeRepository<T> where T : class
    {
        Task<T> CreateMessages(T message);
        Task<bool> DeleteMessage(int idMessage);
        Task<ICollection<T>> GetMessageByRoom(int roomId);
        Task DeleteMessageofUser(int id);
    }
}
