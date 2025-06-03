using Microsoft.AspNetCore.Mvc;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Impl;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class SalaController : Controller
    {
        private readonly ISalaService _salaService;
        public SalaController(ISalaService salaService)
        {
            _salaService = salaService;
        }

        [Breadcrumb("Sala", FromController = typeof(HomeController), FromAction = "Index")]
        [Route("sala")]
        public IActionResult Lista()
        {
            var salas = _salaService.ListaSalas();
            ViewData["Titulo"] = "Lista de Salas";
            return View(salas);
        }

        [Breadcrumb("Registrar", FromAction = "Lista")]
        [Route("sala/formulario")]
        public IActionResult Formulario()
        {
            ViewData["Titulo"] = "Registrar Sala";
            return View();
        }

        [HttpPost]
        [Route("sala/agregar")]
        public IActionResult Registrar(Sala sala)
        {
            if (ModelState.IsValid)
            {
                _salaService.AgregarSala(sala);
                TempData["SalaRegistrada"] = $"Sala {sala.Nombre} fue agregada correctamente";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = "Registrar Sala";
            return View("Formulario", sala);
        }

        [Breadcrumb("Actualizar", FromAction ="Lista")]
        [Route("sala/editar/{id:int}")]
        public IActionResult Editar(int id)
        {
            var sala = _salaService.ObtenerSala(id);
            if (sala == null)
            {
                TempData["SalaNoEncontrada"] = $"Sala con ID {id} no encontrada";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = $"Editar Sala {sala.Nombre}";
            return View(sala);
        }

        [HttpPost]
        [Route("sala/actualizar/{id:int}")]
        public IActionResult Actualizar(int id, Sala model) {
            if (ModelState.IsValid)
            {
                var salaExiste = _salaService.ObtenerSala(id);
                if(salaExiste == null)
                {
                    ViewData["NoExiste"] = "NoExiste";
                    return View("Lista");
                }

                salaExiste.Nombre = model.Nombre;
                salaExiste.Capacidad = model.Capacidad;


                _salaService.ActualizarSala(salaExiste);
                TempData["SalaActualizada"] = $"Sala {model.Nombre} fue actualizada correctamente";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = "Actualizar Sala";
            return View("Editar", model);
        }




        [Breadcrumb("Eliminar", FromAction = "Lista")]
        [Route("sala/eliminar/{id:int}")]
        public IActionResult Confirmacion(int id) { 
            var sala = _salaService.ObtenerSala(id);
            if (sala == null)
            {
                TempData["SalaNoEncontrada"] = $"Sala con ID {id} no encontrada";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = $"Eliminar Sala {sala.Nombre}";
            return View(sala);
        }

        [HttpPost]
        [Route("sala/eliminar/{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var sala = _salaService.ObtenerSala(id);
            if (sala == null)
            {
                TempData["ErrorEliminar"] = "Ocurrio un Error al Eliminar";
                return View("Confirmacion");
            }
            _salaService.EliminarSala(id);
            TempData["SalaEliminada"] = $"Sala {sala.Nombre} fue eliminada correctamente";
            return RedirectToAction("Lista");

        }



    }
}
