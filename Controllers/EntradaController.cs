using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;
using System.Threading.Tasks;

namespace SistemaCineMVC.Controllers
{
    public class EntradaController : Controller
    {
        private readonly IEntradaRepository _entradaRepository;
        private readonly BdCine2025Context _context;
        public EntradaController(IEntradaRepository entradaRepository, BdCine2025Context context)
        {
            _entradaRepository = entradaRepository;
            _context = context;
        }

        [Breadcrumb("Entrada", FromController = typeof(HomeController), FromAction = "Index")]
        [Route("entrada")]
        public IActionResult Index(int? idFuncion, int? idAsiento, string? estado)
        {
            // ** SIEMPRE POPULAR LOS VIEWBAGS PARA LOS DROPDOWNS **
            // Esto asegura que los SelectList siempre tengan datos, incluso si no se está filtrando por ellos.
            ViewData["IdFuncion"] = new SelectList(_context.Funcions, "IdFuncion", "IdFuncion");
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento"); // Asumo que "Fila" es más descriptivo que "IdAsiento" para mostrar.

            List<Entradum> entradas;
            string titulo = "Lista de Entradas"; // Título por defecto

            if (idFuncion.HasValue)
            {
                entradas = _entradaRepository.ListarPorFuncion(idFuncion);
                titulo = $"Entradas de la Función {idFuncion}";
            }
            else if (idAsiento.HasValue)
            {
                entradas = _entradaRepository.ListarPorAsiento(idAsiento);
                titulo = $"Entradas del Asiento {idAsiento}";
            }
            else if (!string.IsNullOrEmpty(estado))
            {
                entradas = _entradaRepository.ListarPorEstado(estado);
                titulo = $"Entradas con estado {estado}";
            }
            else
            {
                entradas = _entradaRepository.GetEntradas(); // Asumo que tienes un método para obtener todas las entradas
            }

            ViewData["Titulo"] = titulo;
            return View(entradas);
        }




















        [Breadcrumb("Función" , FromAction ="Index")]
        [Route("entrada/funcion/{id:int}")]
        public IActionResult DetalleFuncion(int id)
        {

            var funcionSelec = _entradaRepository.VerDetalleFuncion(id);
            return View(funcionSelec);
        }
        [Breadcrumb("Asiento", FromAction = "Index")]
        [Route("entrada/asiento/{id:int}")]
        public IActionResult DetalleAsiento(int id)
        {
            var asientoSelec = _entradaRepository.VerDetalleAsiento(id);
            return View(asientoSelec);
        }

        [Breadcrumb("Detalle", FromAction = "Index")]
        public async Task<IActionResult> DetalleEntrada(int id)
        {
            var entrada = await _entradaRepository.VerDetalleEntrada(id);
            if (entrada == null)
            {
                return NotFound();
            }
            return View(entrada);
        }


        [Breadcrumb("Formulario", FromAction = "Index")]
        [Route("entrada/formulario")]
        public IActionResult Formulario()
        {
            ViewData["Titulo"] = "Formulario Registro de Entrada";
            ViewData["IdFuncion"] = new SelectList(_context.Funcions, "IdFuncion", "IdFuncion");
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento");
            return View();
        }

        public IActionResult Registrar(Entradum model)
        {
            model.Estado = "Disponible";
            if (ModelState.IsValid)
            {
                _entradaRepository.AddEntrada(model);
                
                TempData["RegistroExitoso"] = "Entrada Registrada";
                return RedirectToAction("Index");
            }
            ViewData["IdFuncion"] = new SelectList(_context.Funcions, "IdFuncion", "IdFuncion");
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento");
            return View("Formulario", model);

        }

        [Breadcrumb("Editar" , FromAction ="Index")]
        [Route("entrada/editar/{id}")]
        public IActionResult Editar(int id,Entradum model)
        {
            var seleccionado = _entradaRepository.GetEntradaById(id);
            ViewData["IdFuncion"] = new SelectList(_context.Funcions, "IdFuncion", "IdFuncion");
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento");
            return View(seleccionado);
        }



        [HttpPost]
        public IActionResult Actualizar(int id, Entradum model)
        {
            if (ModelState.IsValid)
            {
                var modelParaActualizar = _entradaRepository.GetEntradaById(id);

                modelParaActualizar.IdFuncion = model.IdFuncion;
                modelParaActualizar.IdAsiento = model.IdAsiento;
                modelParaActualizar.Precio = model.Precio;
                modelParaActualizar.Estado = model.Estado;

                _entradaRepository.UpdateEntrada(modelParaActualizar);
                
                TempData["ActualizarExito"] = "Actualizado con Exito";
                return RedirectToAction("Index");
            }

            ViewData["IdFuncion"] = new SelectList(_context.Funcions, "IdFuncion", "IdFuncion");
            ViewData["IdAsiento"] = new SelectList(_context.Asientos, "IdAsiento", "IdAsiento");
            return View("Editar",model);
        }










    }
}
