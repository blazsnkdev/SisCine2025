using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class AsientoRepository : IAsientoRepository
    {

        private readonly BdCine2025Context _context;

        public AsientoRepository(BdCine2025Context context)
        {
            _context = context;
        }
        public void ActualizarAsiento(Asiento sala)
        {
            _context.Asientos.Update(sala);
            _context.SaveChanges();
        }

        public void AgregarAsiento(Asiento sala)
        {
            _context.Asientos.Add(sala);
            _context.SaveChanges();
        }

        public void EliminarAsiento(int id)
        {
            var asiento = _context.Asientos.FirstOrDefault(a => a.IdAsiento == id);
            if (asiento != null)
            {
                _context.Asientos.Remove(asiento);
                _context.SaveChanges();
            }

        }

        public List<Asiento> ListarAsiento()
        {
            var lista = _context.Asientos
                .Include(a => a.IdSalaNavigation)
                .ToList();

            return lista;
        }

        public List<Asiento> ListarPorSala(int? idSala)
        {
            
            var list = _context.Asientos
                .Where(s => s.IdSala == idSala)
                .Include(sn =>sn.IdSalaNavigation)
                .ToList();

            return list;
        }

        public Asiento ObtenerAsiento(int id)
        {
            var asiento = _context.Asientos.FirstOrDefault(a => a.IdAsiento == id);
            return asiento;
        }
    }
}
