using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IClienteRepository
    {

        List<Cliente> ListaClientes();
        Cliente ObtenerCliente(int id);
        void AgregarCliente(Cliente cliente);
        void ActualizarCliente(/*int id,*/ Cliente cliente);
        void EliminarCliente(int id);

        bool EmailExiste(string email);

        //Cliente BuscarPorNombre(string nombre);
    }
}
