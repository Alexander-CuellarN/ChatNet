using AutoMapper;
using Data.Models;
using Data.ModelsView;
using Microsoft.AspNetCore.Mvc;
using Services.Repositories;

namespace ChatNet.Controllers
{
    public class SalaController : Controller
    {
        private readonly SalaRepository<Sala> _service;
        private readonly IMapper _mapper;

        public SalaController(SalaRepository<Sala> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var salas = _mapper.Map<List<SalaModelView>>(await _service.ListSalas());

            if (!TempData.ContainsKey("userId") || !TempData.ContainsKey("userName"))
                return RedirectToAction("Index", "Home");

            var userLogedId = TempData["userId"].ToString();
            var userLogedName = TempData["userName"].ToString();

            ViewBag.UserName = userLogedName;
            ViewBag.UserId = userLogedId;

            TempData.Keep("userId");
            TempData.Keep("userName");

            if (TempData.ContainsKey("Message")) ViewBag.Message = TempData["message"];
            return View(salas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            if (!TempData.ContainsKey("userId") || !TempData.ContainsKey("userName"))
                return RedirectToAction("Index", "Home");

            var userLogedId = TempData["userId"].ToString();
            var userLogedName = TempData["userName"].ToString();

            ViewBag.UserName = userLogedName;
            ViewBag.UserId = userLogedId;

            TempData.Keep("userId");
            TempData.Keep("userName");

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(SalaModelView modelView)
        {
            if (!ModelState.IsValid) return View(modelView);

            var salaNew = _mapper.Map<Sala>(modelView);
            var status = await _service.CreateSala(salaNew);

            if (!status) return View(modelView);
            TempData.Add("message", $"la sala {salaNew.Title} ha sido creada con exito");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var salaFounded = await _service.FindSalaById(id);
            if (salaFounded == null)
            {
                TempData.Add("message", "No se ha podido encontrar la sala selecionada.");
                return RedirectToAction(nameof(Index));
            }

            return View(_mapper.Map<SalaModelView>(salaFounded));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(SalaModelView modelView)
        {
            if (!ModelState.IsValid) return View(modelView);
            var sala = _mapper.Map<Sala>(modelView);
            var result = await _service.UpdateSala(sala);
            if (!result) return View(modelView);
            TempData.Add("message", $" la sala {sala.Title} ha sido modificada con exito");
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ResponseSchema = new ResponseGeneric<SalaModelView>();
            var sala = await _service.FindSalaById(id);

            if (sala == null)
            {
                ResponseSchema.Message = "No se ha podido encontrar la sala selecionada";
                return BadRequest(ResponseSchema);
            }

            var servicesResponse = await _service.DeleteSala(sala);

            if (!servicesResponse)
            {
                ResponseSchema.Message = "Ha ocurrido un error eliminando la sala.";
                return BadRequest(ResponseSchema);
            }

            return NoContent();
        }
    }
}
