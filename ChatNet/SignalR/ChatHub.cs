using Data.Models;
using Microsoft.AspNetCore.SignalR;
using Services.Repositories;
using Services.Services;

namespace ChatNet.SignalR
{
    public class ChatHub : Hub
    {
        private readonly mensajeRepository<Mensaje> _mensajes;
        private readonly UsuarioRepository<Usuario> _usuarios;

        public ChatHub(mensajeRepository<Mensaje> mensajes,
            UsuarioRepository<Usuario> usuarios)
        {
            _mensajes = mensajes;
            _usuarios = usuarios;
        }

        public async Task instanceConnection(int room, string Name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room.ToString());
            await Clients.Group(room.ToString()).SendAsync("newConnect", Name);
        }

        public async Task SendMessage(int room, int userId, string Message)
        {
            var userModel = await _usuarios.FindUserById(userId);
            var newMensaje = new Mensaje()
            {
                Contenido = Message,
                SalaId = room,
                UsuarioId = userModel.UsuarioID
            };

            var mensaje = await _mensajes.CreateMessages(newMensaje);

            await Clients.Group(room.ToString()).SendAsync("newMessage", 
                Message, 
                mensaje.MensajeId, 
                userModel.NickName);
        }

        public async Task DeleteMessage(int room, int messageId)
        {
            var responseDelete = await _mensajes.DeleteMessage(messageId);

            await Clients.Group(room.ToString()).SendAsync("DeleteMessage", 
                responseDelete, 
                messageId);
        }
    }
}
