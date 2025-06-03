using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IClienteService
    {

        List<Cliente> ListaClientes();
        Cliente ObtenerCliente(int id);
        void AgregarCliente(Cliente cliente);
        void ActualizarCliente(/*int id,*/ Cliente cliente);
        void EliminarCliente(int id);
        //Cliente BuscarPorNombre(string nombre);
    }
}
