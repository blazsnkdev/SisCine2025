using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IVentaRepository
    {

        Task CrearVenta(Ventum venta);

        Task Save();

    }
}
