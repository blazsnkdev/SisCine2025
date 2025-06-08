using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Interfaces;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public void Agregar(Cliente cliente)
        {
            // 1. Formatear el teléfono: por ejemplo, quitar espacios y agregar guiones
            string telefonoSinEspacios = cliente.Telefono.Replace(" ", "");
            string telefonoFormateado = FormatearTelefono(telefonoSinEspacios);
            cliente.Telefono = telefonoFormateado;

            // 2. Verificar que el email sea único
            bool emailExiste = _clienteRepository.EmailExiste(cliente.Email);
            if (emailExiste)
            {
                throw new ArgumentException("El correo electrónico ya está registrado.");
            }

            // 3. Agregar cliente
            _clienteRepository.AgregarCliente(cliente);

        }


        private string FormatearTelefono(string telefono)
        {
            if (telefono.Length == 10) {
                return $"({telefono.Substring(0, 3)}) {telefono.Substring(3, 3)}-{telefono.Substring(6)}";
            }
            else {
                return telefono;
            }
        }


    }
}
