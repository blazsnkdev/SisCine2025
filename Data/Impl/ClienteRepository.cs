using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class ClienteRepository : IClienteRepository
    {

        private readonly BdCine2025Context _context;
        public ClienteRepository(BdCine2025Context context)
        {
            _context = context;
        }

        public List<Cliente> ListaClientes()
        {
            var lista = _context.Clientes.ToList();
            return lista;
        }

        public Cliente ObtenerCliente(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.IdCliente == id);
            
            return cliente;
        }
        public void AgregarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }
            _context.Clientes.Add(cliente);
            _context.SaveChanges();

        }


        public void EliminarCliente(int id)
        {
            var clienteExistente = _context.Clientes.FirstOrDefault(c =>c.IdCliente == id);
            if (clienteExistente != null)
            {
                _context.Clientes.Remove(clienteExistente);
                _context.SaveChanges();
            }
        }

        public void ActualizarCliente(/*int id,*/ Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public bool EmailExiste(string email)
        {
            return _context.Clientes.Any(c => c.Email == email);

        }

        //public Cliente BuscarPorNombre(string nombre)
        //{
        //    var cliente = _context.Clientes.FirstOrDefault(c => c.Nombre == nombre);

        //    return cliente;

        //}
    }
}
