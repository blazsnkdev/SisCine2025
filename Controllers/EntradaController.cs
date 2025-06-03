using Microsoft.AspNetCore.Mvc;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Controllers
{
    public class EntradaController : Controller
    {
        private readonly IEntradaRepository _entradaRepository;
        public EntradaController(IEntradaRepository entradaRepository)
        {
            _entradaRepository = entradaRepository;
        }

        public IActionResult Index()
        {
            var entradas = _entradaRepository.GetEntradas();

            return View(entradas);
        }

        public IActionResult DetalleFuncion(int id)
        {

            var funcionSelec = _entradaRepository.VerDetalleFuncion(id);
            return View(funcionSelec);
        }

        public IActionResult DetalleAsiento(int id)
        {
            var asientoSelec = _entradaRepository.VerDetalleAsiento(id);
            return View(asientoSelec);
        }















    }
}
