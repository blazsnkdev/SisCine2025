using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Interfaces;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class AsientoController : Controller
    {

        private readonly IAsientoRepository _asientoRepository;
        private readonly BdCine2025Context _context;
        private readonly IAsientoService _asientoService;
        public AsientoController(IAsientoRepository asientoRepository, BdCine2025Context context, IAsientoService asientoService)
        {
            _asientoRepository = asientoRepository;
            _context = context;
            _asientoService = asientoService;
        }

        [Breadcrumb("Asiento", FromController =typeof(HomeController), FromAction ="Index")]
        [Route("asiento/{id:int?}")]
        public IActionResult Lista(int? idSala)
        {
            if (idSala.HasValue)
            {
                var sala = _asientoRepository.ObtenerAsiento(idSala.Value);
                var listaParam = _asientoRepository.ListarPorSala(idSala);
                ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
                ViewData["Titulo"] = $"Asientos en la Sala";
                return View(listaParam);
            }
            else
            {

                var lista = _asientoRepository.ListarAsiento();
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
            
            return View();
        }

        [HttpPost]
        [Route("asiento/registrar")]
        public IActionResult Registrar(Asiento model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _asientoService.Agregar(model);
                    TempData["ExitoRegistro"] = "Registrado Correctamente";
                    return RedirectToAction("Lista");
                }
                catch (ArgumentException ex)
                {
                    ViewData["MensajeError"] = ex.Message;
                }
            }
                ViewData["IdSala"] = new SelectList(_context.Salas, "IdSala", "Nombre");
                
                return View("Formulario", model);
            
        }

        [Breadcrumb("Editar", FromAction ="Lista")]
        [Route("asiento/editar/{id:int}")]
        public IActionResult Editar(int id, Asiento model)
        {
            var asiento = _asientoRepository.ObtenerAsiento(id);
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
                var asiento = _asientoRepository.ObtenerAsiento(id);
                
                asiento.IdSala = model.IdSala;
                asiento.Fila = model.Fila;
                asiento.Numero = model.Numero;

                _asientoRepository.ActualizarAsiento(asiento);
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
            var selec = _asientoRepository.ObtenerAsiento(id);
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
            var selec = _asientoRepository.ObtenerAsiento(id);
            if (selec == null)
            {
                ViewData["ErrorEliminar"] = "Error al eliminar";
                return View("Confirmacion");
            }
            _asientoRepository.EliminarAsiento(id);
            TempData["ExitoEliminado"]= "Eliminado Exitoso";
            return RedirectToAction("Lista");
            
        }
      



    }
}
