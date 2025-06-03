using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class FuncionController : Controller
    {

        private readonly IFuncionService _funcionService;
        //private readonly IPeliculaService _peliculaService;
        //private readonly ISalaService _salaService;

        private readonly BdCine2025Context _dbcine2025Context;

        public FuncionController(IFuncionService funcionService, BdCine2025Context dbcine2025Context)
        {
            _funcionService = funcionService;
            _dbcine2025Context = dbcine2025Context;
        }

        [Breadcrumb("Funcion", FromController = typeof(HomeController), FromAction = "Index")]
        [Route("funcion")]
        public IActionResult Lista()
        {
            var lista = _funcionService.Lista();
            ViewData["Titulo"] = "Lista de Funciones";
            return View(lista);
        }

        [Breadcrumb("Registrar",FromAction ="Lista")]
        [Route("funcion/formulario")]
        public IActionResult Formulario()
        {

            ViewData["IdSala"] = new SelectList(_dbcine2025Context.Salas,"IdSala","Nombre");
            ViewData["IdPelicula"] = new SelectList(_dbcine2025Context.Peliculas, "IdPelicula", "Titulo");
            ViewData["Titulo"] = "Registrar Funcion";
            return View();
        }

        [HttpPost]
        [Route("funcion/agregar")]
        public IActionResult Registrar(Funcion model)
        {
        
            if (ModelState.IsValid)
            {
                _funcionService.Agregar(model);
                TempData["ExitoRegistro"] = "Registrado Correctamente";
                return RedirectToAction("Lista");
            }
            else
            {
                ViewData["IdSala"] = new SelectList(_dbcine2025Context.Salas, "IdSala", "Nombre");
                ViewData["IdPelicula"] = new SelectList(_dbcine2025Context.Peliculas, "IdPelicula", "Titulo");
                return View("Formulario", model);
            }
        }


        [Breadcrumb("Editar",FromAction = "Lista")]
        [Route("funcion/editar/{id}")]
        public IActionResult Editar(int id)
        {
            var funcion = _funcionService.ObtenerFuncion(id);

            if(funcion == null)
            {
                return NotFound();
            }

            ViewData["IdSala"] = new SelectList(_dbcine2025Context.Salas, "IdSala", "Nombre");
            ViewData["IdPelicula"] = new SelectList(_dbcine2025Context.Peliculas, "IdPelicula", "Titulo");

            return View(funcion);
            
        }


        [HttpPost]
        [Route("funcion/actualizar/{id}")]
        public IActionResult Actualizar(int id, Funcion model)
        {
            
            if (ModelState.IsValid)
            {

                var funcionObj = _funcionService.ObtenerFuncion(id);
                if(funcionObj == null)
                {
                    return NotFound();
                }
                funcionObj.IdPelicula = model.IdPelicula;
                funcionObj.IdSala = model.IdSala;
                funcionObj.Fecha = model.Fecha;
                funcionObj.HoraInicio = model.HoraInicio;
                funcionObj.HoraFin = model.HoraFin;

                _funcionService.Actualizar(funcionObj);
                TempData["ExitoActualiado"] = "Se Actualizo correctamente";
                return RedirectToAction("Lista");
            }
            ViewData["IdSala"] = new SelectList(_dbcine2025Context.Salas, "IdSala", "Nombre");
            ViewData["IdPelicula"] = new SelectList(_dbcine2025Context.Peliculas, "IdPelicula", "Titulo");
            ViewData["Titulo"] = "Editar Funcion";
            return View("Lista", model);
        }

        [Breadcrumb("Eliminar", FromAction = "Lista")]
        [Route("funcion/confirmacion/{id}")]
        public IActionResult Confirmacion(int id)
        {
            var persona = _funcionService.ObtenerFuncion(id);
            if(persona == null)
            {
                ViewData["Titulo"] = "Editar Funcion";
                TempData["Noexiste"] = "El usuario No Eciste";//devuelve la misma vista con una not
                return View("Lista");
            }
            ViewData["Titulo"] = "Editar Funcion";
            return View(persona);
        }


        [HttpPost]
        [Route("funcion/eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var persona = _funcionService.ObtenerFuncion(id);//encotrar registro
            
            if (persona == null)//si el registro no existe
            {
                TempData["ErrorEliminar"] = "Error al Eliminar";//devuelve la misma vista con una not
                return View("Confirmacion") ;
            }
            _funcionService.Eliminar(id);//se elimna
            TempData["ExitoEliminado"] = "Eliminado Correctamente";//mensaje conf
            return RedirectToAction("Lista");//devolver la lista
        }



    }
}
