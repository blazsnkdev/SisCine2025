using Microsoft.EntityFrameworkCore;
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

        public Task<DetalleVentum?> GetDetalleVentaById(int id)
        {
            var detalle = _context.DetalleVenta
                .Include(dv => dv.IdEntradaNavigation)
                .ThenInclude(e => e.IdFuncionNavigation)
                .ThenInclude(f => f.IdPeliculaNavigation)
                .FirstOrDefaultAsync(dv => dv.IdDetalle == id);
            return detalle;
        }

        // Inside your IVentaRepository or concrete implementation
        public async Task<Ventum?> GetVentaById(int id)
        {
            var venta = await _context.Venta
                .Include(v => v.DetalleVenta)
                    .ThenInclude(dv => dv.IdEntradaNavigation)
                        .ThenInclude(e => e.IdFuncionNavigation)
                            .ThenInclude(f => f.IdPeliculaNavigation) // Include Pelicula
                .Include(v => v.DetalleVenta) // Re-include to branch off to another ThenInclude
                    .ThenInclude(dv => dv.IdEntradaNavigation)
                        .ThenInclude(e => e.IdFuncionNavigation)
                            .ThenInclude(f => f.IdSalaNavigation) // Include Sala
                .Include(v => v.DetalleVenta) // Re-include again
                    .ThenInclude(dv => dv.IdEntradaNavigation)
                        .ThenInclude(e => e.IdAsientoNavigation) // Include Asiento
                .Include(v => v.IdClienteNavigation) // Include Cliente
                .FirstOrDefaultAsync(v => v.IdVenta == id);
            return venta;
        }





        public async Task Save()
        {
           await _context.SaveChangesAsync();  
        }
    }
}
