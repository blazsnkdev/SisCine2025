using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IVentaRepository
    {

        Task CrearVenta(Ventum venta);

        Task Save();

        Task<Ventum?> GetVentaById(int id);
        Task<DetalleVentum?> GetDetalleVentaById(int id);


    }
}
