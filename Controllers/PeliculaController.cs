using Microsoft.AspNetCore.Mvc;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly IPeliculaRepository _peliculaService;
        public PeliculaController(IPeliculaRepository peliculaService)
        {
            _peliculaService = peliculaService;
        }

        [Breadcrumb("Pelicula", FromController = typeof(HomeController), FromAction = "Index")]
        [Route("pelicula")]
        public IActionResult Lista()
        {
            ViewData["Titulo"] = "Lista de Peliculas";
            var lista = _peliculaService.ListaPeliculas();
            return View(lista);
        }


        [Breadcrumb("Registrar", FromAction = "Lista")]
        [Route("pelicula/formulario")]
        public IActionResult Formulario()
        {
            ViewData["Titulo"] = "Registrar Pelicula";
            return View();
        }

        [HttpPost]
        [Route("pelicula/agregar")]
        public IActionResult Agregar(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                _peliculaService.AgregarPelicula(pelicula);
                TempData["PeliculaRegistrada"] = $"Pelicula {pelicula.Titulo} fue agregado correctamente";
                return RedirectToAction("Lista");
            }
            return View("Formulario", pelicula);
        }

        [Breadcrumb("Editar", FromAction = "Lista")]
        [Route("pelicula/editar/{id}")]
        public IActionResult Editar(int id)
        {
            var pelicula = _peliculaService.ObtenerPelicula(id);
            if (pelicula == null)
            {
                return NotFound(); //Agregar Vista de Error
            }
            ViewData["Titulo"] = $"Editar Pelicula {pelicula.Titulo}";
            return View(pelicula);
        }

        [HttpPost]
        [Route("pelicula/actualizar/{id}")]
        public IActionResult Actualizar(int id,Pelicula model)
        {
            if (ModelState.IsValid)
            {

                var peliculaExistente = _peliculaService.ObtenerPelicula(id);
                if (peliculaExistente == null)
                {
                    return NotFound(); // No existe, no se puede actualizar
                }
                peliculaExistente.Titulo = model.Titulo;
                peliculaExistente.Genero = model.Genero;
                peliculaExistente.Duracion = model.Duracion;
                peliculaExistente.Clasificacion = model.Clasificacion;
                peliculaExistente.Sinopsis = model.Sinopsis;

    
                _peliculaService.ActualizarPelicula(peliculaExistente);
                TempData["PeliculaActualizada"] = $"Pelicula {model.Titulo} fue actualizada correctamente";
                return RedirectToAction("Lista");
            }
            return View("Editar", model);

        }

        [Breadcrumb("Eliminar", FromAction = "Lista")]
        [Route("pelicula/eliminar/{id}")]
        public IActionResult Confirmacion(int id)
        {
            var pelicula = _peliculaService.ObtenerPelicula(id);
            if(pelicula == null)
            {
                TempData["PeliculaNoEncontrada"] = $"Pelicula con ID {id} no encontrada";
                return View("Lista"); 
            }
            ViewData["Titulo"] = $"Titulo de Pelicula {pelicula.Titulo}";
            return View(pelicula);
        }

        [HttpPost]
        [Route("pelicula/eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var pelicula = _peliculaService.ObtenerPelicula(id);
            if (pelicula == null)
            {
                TempData["ErrorEliminar"] = "Ocurrio un Error al Eliminar";
                return View("Confirmacion");
            }
            _peliculaService.EliminarPelicula(id);
            TempData["PeliculaEliminada"] = $"Pelicula {pelicula.Titulo} fue eliminada correctamente";
            return RedirectToAction("Lista");

        }


        }
}
