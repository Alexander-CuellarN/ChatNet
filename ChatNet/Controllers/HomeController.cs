using AutoMapper;
using ChatNet.Models;
using Data.Models;
using Data.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Services.Repositories;
using System.Diagnostics;
using System.Text.Json;

namespace ChatNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UsuarioRepository<Usuario> _service;
        private readonly IMapper _mapper;
        private readonly SalaRepository<Sala> _salaRepository;
        private readonly mensajeRepository<Mensaje> _mensajeRepository;

        public HomeController(
            ILogger<HomeController> logger,
            UsuarioRepository<Usuario> service,
            IMapper mapper,
            SalaRepository<Sala> salaRepository,
            mensajeRepository<Mensaje> mensajeRepository)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
            _salaRepository = salaRepository;
            _mensajeRepository = mensajeRepository;
        }

        public IActionResult Index()
        {
            if (TempData.ContainsKey("userId") && TempData.ContainsKey("userName"))
                return RedirectToAction("Main");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UsuarioModelView login)
        {

            if (!ModelState.IsValid)
                return View(login);

            var isARegisterUser = await _service.FindAUser(login.nickName);

            if (isARegisterUser == null)
            {
                var user = _mapper.Map<Usuario>(login);
                await _service.CreateUSuario(user);
                isARegisterUser = await _service.FindAUser(login.nickName);
            }

            TempData.Add("userId", isARegisterUser.UsuarioID);
            TempData.Add("userName", isARegisterUser.NickName);

            return RedirectToAction("Main");
        }

        [HttpGet]
        public async Task<IActionResult> Main()
        {

            if (!TempData.ContainsKey("userId") || !TempData.ContainsKey("userName"))
                return RedirectToAction("Index");

            var userLogedId = TempData["userId"].ToString();
            var userLogedName = TempData["userName"].ToString();

            if( userLogedId == null)
                return RedirectToAction("Index");

            var userLogin = await _service.FindAUser(userLogedName);

            var UserResponse = _mapper.Map<UsuarioModelView>(userLogin);
            var salas = _mapper.Map<List<SalaModelView>>(await _salaRepository.ListSalas());

            ViewBag.Salas = salas;
            ViewBag.UserName = userLogin.NickName;
            ViewBag.UserId = userLogin.UsuarioID;

            TempData.Keep("userId");
            TempData.Keep("userName");
            return View(UserResponse);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMessagesByRoom(int id)
        {
            var response = new ResponseGeneric<MessagesMV>() { Message = "Listado de mensajes" };
            var messageList = await _mensajeRepository.GetMessageByRoom(id);

            response.Data = _mapper.Map<List<MessagesMV>>(messageList);
            return Ok(response);
        }
    }
}
