using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class VentaRepository : IVentaRepository
    {

        private readonly BdCine2025Context _context;
        public VentaRepository(BdCine2025Context context)
        {
            _context = context;
        }
        public async Task CrearVenta(Ventum venta)
        {
            await _context.Venta.AddAsync(venta);
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();  
        }
    }
}
