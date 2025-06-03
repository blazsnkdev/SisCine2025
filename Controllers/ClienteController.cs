using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;
using SmartBreadcrumbs.Attributes;

namespace SistemaCineMVC.Controllers
{
    public class ClienteController : Controller
    {


        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [Breadcrumb("Cliente", FromController = typeof(HomeController), FromAction = "Index")]
        [Route("cliente")]
        public IActionResult Lista()
        {
            ViewData["Titulo"] = "Lista de Clientes";
            var lista = _clienteService.ListaClientes();
            return View(lista);
        }

        [Breadcrumb("Detalle", FromAction = "Lista")]
        [Route("cliente/{id}")]
        public IActionResult Detalle(int id)
        {
            var cliente = _clienteService.ObtenerCliente(id);
            if (cliente == null)
            {
                TempData["DetalleNoEncontrado"] = $"Cliente con ID {id} no encontrado";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = $"Detalle del Cliente {cliente.Nombre}";
            return View(cliente);
        }

        [Breadcrumb("Registro", FromAction = "Lista")]
        [Route("cliente/formulario")]
        public IActionResult Registrar()
        {
            ViewData["Titulo"] = "Registrar Cliente";
            return View();
        }

        [HttpPost]
        [Route("cliente/agregar")]
        public IActionResult Registrar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteService.AgregarCliente(cliente);
                TempData["ClienteRegistrado"] = $"Cliente {cliente.Nombre} fue agregado correctamente";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = "Registrar Cliente";
            return View(cliente);
        }

        [Breadcrumb("Editar", FromAction = "Detalle")]
        [Route("cliente/editar/{id}")]
        public IActionResult Editar(int id)
        {
            var cliente = _clienteService.ObtenerCliente(id);
            if (cliente == null)
            {
                TempData["DetalleNoEncontrado"] = $"Cliente con ID {id} no encontrado";
                return RedirectToAction("Lista");
            }
            ViewData["Titulo"] = $"Editar Cliente {cliente.Nombre}";
            return View(cliente);
        }


       
        [HttpPost]
        [Route("cliente/actualizar/{id}")] 
        public IActionResult Actualizar(int id, Cliente model)
        {
           
            if (ModelState.IsValid)
            {

                var clienteExistente = _clienteService.ObtenerCliente(id);
                if (clienteExistente == null)
                {
                    TempData["DetalleNoEncontrado"] = $"Cliente con ID {id} no encontrado";
                    return RedirectToAction("Lista");
                }

                clienteExistente.Apellido = model.Apellido;
                clienteExistente.Nombre = model.Nombre;
                clienteExistente.Telefono = model.Telefono;
                clienteExistente.Email = model.Email;


                    _clienteService.ActualizarCliente(clienteExistente); 
                    TempData["ClienteActualizado"] = $"Cliente {model.Nombre} fue actualizado correctamente"; 
                    return RedirectToAction("Lista");       
            }
            ViewData["Titulo"] = "Actualizar Sala";
            return View("Editar", model);
        }


        [HttpPost]
        [Route("cliente/eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            _clienteService.EliminarCliente(id);
            TempData["ClienteEliminado"] = "Cliente eliminado correctamente";
            return RedirectToAction("Lista");
        }


        //[HttpGet]
        //[Route("cliente/buscar/{nombre}")]
        //public IActionResult Buscar(string nombre)
        //{
        //    var cliente = _clienteService.BuscarPorNombre(nombre);
        //    if (cliente != null)
        //    {
                
        //        return View("Detalle", cliente);
        //    }
        //        ViewData["Mensaje"] = $"Usuario {nombre} no encontrado";
        //        return View("Lista", _clienteService.ListaClientes());
            
        //}



    }
}
