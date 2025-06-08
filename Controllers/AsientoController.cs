using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class AsientoController : Controller
    {

        private readonly IAsientoRepository _asientoService;
        private readonly BdCine2025Context _context;
        public AsientoController(IAsientoRepository servicio, BdCine2025Context context)
        {
            _asientoService = servicio;
            _context = context;
        }

        [Breadcrumb("Asiento", FromController =typeof(HomeController), FromAction ="Index")]
        [Route("asiento/{id:int?}")]
        public IActionResult Lista(int? idSala)
        {
            if (idSala.HasValue)
            {
                var sala = _asientoService.ObtenerAsiento(idSala.Value);
                var listaParam = _asientoService.ListarPorSala(idSala);
                ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
                ViewData["Titulo"] = $"Asientos en la Sala";
                return View(listaParam);
            }
            else
            {

                var lista = _asientoService.ListarAsiento();
                ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
                ViewData["Titulo"] = "Lista Asientos Por Sala";
                return View(lista);
            }
        }




        [Breadcrumb("Registro", FromAction ="Lista")]
        [Route("asiento/formulario")]
        public IActionResult Formulario()
        {
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
            ViewData["Titulo"] = "Registrar Asiento";
            return View();
        }

        [HttpPost]
        [Route("asiento/registrar")]
        public IActionResult Registrar(Asiento model)
        {
            if (ModelState.IsValid)
            {
                _asientoService.AgregarAsiento(model);
                TempData["ExitoRegistro"] = "Registrado Correctamente";
                return RedirectToAction("Lista");
            }
            else
            {
                ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
                ViewData["ErrorRegistro"] = "Error al Registrar";//en la vista del form
                return View("Formulario", model);
            }
        }

        [Breadcrumb("Editar", FromAction ="Lista")]
        [Route("asiento/editar/{id:int}")]
        public IActionResult Editar(int id, Asiento model)
        {
            var asiento = _asientoService.ObtenerAsiento(id);
            if (asiento == null)
            {
                ViewData["NoEncontrado"] = "No se encuentra el ID";
                return View("Lista");
            }
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
            return View(asiento);
        }
        [HttpPost]
        public IActionResult Actualizar(int id,Asiento model)
        {

            if(ModelState.IsValid)
            {
                var asiento = _asientoService.ObtenerAsiento(id);
                
                asiento.IdSala = model.IdSala;
                asiento.Fila = model.Fila;
                asiento.Numero = model.Numero;

                _asientoService.ActualizarAsiento(asiento);
                TempData["ExitoActualiado"] = "Se Actualizo correctamente";
                return RedirectToAction("Lista");
            }
            ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
            ViewData["Titulo"] = "Editar Funcion";
            return View("Lista",model);

        }

        [Breadcrumb("Eliminar",FromAction ="Lista")]
        [Route("asiento/confirmacion/{id:int}")]
        public IActionResult Confirmacion(int id)
        {
            var selec = _asientoService.ObtenerAsiento(id);
            if(selec == null)
            {

                return View("Lista");
            }
            ViewData["Titulo"] = "Eliminar Asiento";
            return View(selec);
        }

        [HttpPost]
        [Route("asiento/eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var selec = _asientoService.ObtenerAsiento(id);
            if (selec == null)
            {
                ViewData["ErrorEliminar"] = "Error al eliminar";
                return View("Confirmacion");
            }
            _asientoService.EliminarAsiento(id);
            TempData["ExitoEliminado"]= "Eliminado Exitoso";
            return RedirectToAction("Lista");
            
        }
      



    }
}
