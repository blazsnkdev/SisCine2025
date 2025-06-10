using Microsoft.AspNetCore.Mvc;
using SistemaCineMVC.Data.Repo;
using SistemaCineMVC.Models;
using SmartBreadcrumbs.Attributes;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SistemaCineMVC.Controllers
{
    [DefaultBreadcrumb("Inicio")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _UoW;
        public HomeController(IUnitOfWork uow)
        {
            _UoW = uow;
        }

        public async Task<IActionResult> Index()
        {
            var countClientes = await _UoW.Cliente.CountEntityAsync();
            var countPeliculas = await _UoW.Pelicula.CountEntityAsync();
            var countDetalleVenta = await _UoW.DetalleVenta.CountEntityAsync();
            var countFunciones = await _UoW.Funcion.CountEntityAsync();

            ViewBag.CountClientes = countClientes;
            ViewBag.CountPeliculas = countPeliculas;
            ViewBag.CountDetalleVentas = countDetalleVenta;
            ViewBag.CountFunciones = countFunciones;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
