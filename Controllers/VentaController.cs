using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;
using SistemaCineMVC.Models.ViewModels;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class VentaController : Controller
    {

        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteRepository _clienteService;
        private readonly IEntradaRepository _entradaRepository;
        private readonly BdCine2025Context _context;
        public VentaController(IVentaRepository ventaRepository, IClienteRepository clienteService, IEntradaRepository entradaRepository, BdCine2025Context context)
        {
            _ventaRepository = ventaRepository;
            _clienteService = clienteService;
            _entradaRepository = entradaRepository;
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpGet]
        [Breadcrumb("Venta", FromController = typeof(HomeController), FromAction = "Index")]
        public async Task<IActionResult> CrearVenta()
        {
            
            ViewBag.Clientes = new SelectList(_clienteService.ListaClientes(), "IdCliente", "Nombre");
            ViewBag.Entradas = await _entradaRepository.EntradasDisponibles(); 
            var viewModel = new CrearVentaViewModel();
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearVenta(CrearVentaViewModel viewModel)
        {
            //CARGAR VIEW BAGS, LISTA Y CHEKBOX
            ViewBag.Clientes = new SelectList(_clienteService.ListaClientes(), "IdCliente", "Nombre");
            ViewBag.Entradas = await _entradaRepository.EntradasDisponibles();

            //SI NO SE SELECIONO UN UNA ENTRADA
            if (viewModel.EntradasSeleccionadasIds == null || !viewModel.EntradasSeleccionadasIds.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos una entrada.");//ESTO PUEDO CAMBIAR
                return View(viewModel);
            }
            //SI EL MODELO NO ES VALIDO
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            //CREAMOS UNA LISTA DE DETALLE
            var detalles = new List<DetalleVentum>();
            decimal? totalVenta = 0m;//INICIALIZAMOS EL TOTAL DE VENTA 

            foreach (var entradaId in viewModel.EntradasSeleccionadasIds)
            {
                var entrada = /*await*/ _entradaRepository.GetEntradaById(entradaId);// OBTENEMOS LA ENTRADA POR ID

                if (entrada == null)//SI LA ENTRADA ES NULA
                {
                    ModelState.AddModelError("", $"La entrada con ID {entradaId} no fue encontrada o ya no está disponible.");
                    return View(viewModel);
                }

                if (entrada.Estado != "Disponible" && entrada.Estado != "Reservado")//SI LA ENTRADA ES DIFERENTE A DISPONIBLE O ESTA RESEVADA NO APLICA
                {
                   
                    string entryDescription = (entrada.IdFuncionNavigation != null && entrada.IdAsientoNavigation != null)
                        ? $"Función: {entrada.IdFuncionNavigation.IdPeliculaNavigation?.Titulo} - Asiento: {entrada.IdAsientoNavigation.Fila}{entrada.IdAsientoNavigation.Numero}"
                        : $"ID: {entradaId}";

                    ModelState.AddModelError("", $"La entrada ({entryDescription}) no está disponible (Estado actual: {entrada.Estado}).");//MUESTRA EL ERROR CON EL DETALLE
                    return View(viewModel); //RETURN VISTA CON LA DATA
                }

                detalles.Add(new DetalleVentum
                {
                    IdEntrada = entrada.IdEntrada,
                    PrecioUnitario = entrada.Precio
                });

                totalVenta += entrada.Precio;


                await _entradaRepository.MarcarEntradaVendida(entrada.IdEntrada);
            }
            
            var venta = new Ventum
            {
                IdCliente = viewModel.IdCliente,
                FechaVenta = DateTime.Now,
                Total = totalVenta,
                DetalleVenta = detalles 
            };

            
            await _ventaRepository.CrearVenta(venta); 
            await _ventaRepository.Save(); 

            TempData["VentaExitosa"] = "Venta registrada exitosamente!";
            return RedirectToAction("Confirmacion", new { id = venta.IdVenta });
        }


        [Breadcrumb("Confirmación", FromAction = "CrearVenta")]
        public IActionResult Confirmacion(int id)
        {
            ViewData["IdVenta"] = id;
            return View();
        }

        [Breadcrumb("Detalle", FromAction = "CrearVenta")]
        public async Task<IActionResult> Detalle(int id)
        {
           
            var venta = await _ventaRepository.GetVentaById(id); // Use your existing method

            if (venta == null)
            {
                return NotFound(); // Or RedirectToAction("Index") with an error message
            }

            // Pass the entire Ventum object to the view
            return View(venta);
        }













    }
}
