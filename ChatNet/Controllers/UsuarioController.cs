using AutoMapper;
using Data.Models;
using Data.ModelsView;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories;

namespace ChatNet.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository<Usuario> _service;
        private IMapper _mapper;
        private mensajeRepository<Mensaje> _messageServices;

        public UsuarioController(UsuarioRepository<Usuario> service,
            IMapper mapper,
            mensajeRepository<Mensaje> messageServices)
        {
            _service = service;
            _mapper = mapper;
            _messageServices = messageServices;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UsuarioModelView usuario)
        {
            var response = new ResponseGeneric<UsuarioModelView>();

            if (!ModelState.IsValid) return BadRequest(response);

            var isAchoiseName = await _service.FindAUser(usuario.nickName);

            if (isAchoiseName != null)
            {
                response.Message = "El nombre se usaurio ya ha sido selecionado";
                return BadRequest(response);
            }

            var user = await _service.FindUserById(usuario.UsurioId);
            user.NickName = usuario.nickName;
            var status = await _service.UdpateUsuario(user);

            if (!status)
            {
                response.Message = "No se ha podido Modificar el usuario";
                return BadRequest(response);
            }

            response.Message = "Se ha modificado el usuario Correctamente";
            TempData["userName"] = user.NickName;
            return Ok(response);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = new ResponseGeneric<Usuario>();

            var result = await _service.DeleteUsuario(id);

            if (!result)
            {
                response.Message = "No se ha podido eliminar el usuario";
                return BadRequest();
            }

            await _messageServices.DeleteMessageofUser(id);
            TempData.Remove("userId");
            TempData.Remove("userName");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SingOut()
        {
            TempData.Remove("userId");
            TempData.Remove("userName");
            return RedirectToAction("Index", "Home");
        }
    }
}
