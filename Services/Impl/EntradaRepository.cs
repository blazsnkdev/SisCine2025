using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class EntradaRepository : IEntradaRepository
    {
        private readonly BdCine2025Context _context;

        public EntradaRepository(BdCine2025Context context)
        {
            _context = context;
        }

        public void AddEntrada(Entradum entrada)
        {
            
            _context.Entrada.Add(entrada);
            _context.SaveChanges();
        }

        public void DeleteEntrada(int id)
        {
            var selec = _context.Entrada
                .FirstOrDefault(e => e.IdEntrada == id);
            _context.Entrada.Remove(selec);
        }

        public Entradum? GetEntradaById(int id)
        {
            var entrada = _context.Entrada.ToList()
                .FirstOrDefault(e => e.IdEntrada == id);
            return entrada;
        }

        public List<Entradum> GetEntradas()
        {
            var list = _context.Entrada
                .ToList();
            return list;
        }

        public List<Entradum> ListarPorAsiento(int? idAsiento)
        {
            var list = _context.Entrada
                .Where(e => e.IdAsiento == idAsiento)
                .Include(e => e.IdAsientoNavigation)
                .ToList();
            return list;
        }

        public List<Entradum> ListarPorEstado(string estado)
        {
            var list = _context.Entrada
                .Where(e => e.Estado == estado)
                .ToList();
            return list;
        }

        public List<Entradum> ListarPorFuncion(int? idFuncion)
        {
            var list = _context.Entrada
                .Where(f => f.IdFuncion == idFuncion)
                .Include(fn=>fn.IdFuncionNavigation)
                .ToList();

            return list;
        }




        public async Task MarcarEntradaVendida(int id)
        {
            var entrada = await _context.Entrada
                .FirstOrDefaultAsync(e => e.IdEntrada == id); 

            if (entrada != null)
            {
                entrada.Estado = "Vendida";
         
                await _context.SaveChangesAsync(); 
            }
         
        }

        public void ReservarEntrada(Entradum entrada)
        {
            var seleccionado = _context.Entrada
                .FirstOrDefault(e => e.IdEntrada == entrada.IdEntrada);
            seleccionado.Estado = "Reservado";
            _context.SaveChanges();
        }

        public void UpdateEntrada(Entradum entrada)
        {
            _context.Entrada.Update(entrada);
            _context.SaveChanges();
            
        }

        public Asiento? VerDetalleAsiento(int id)
        {
            var detalleAsientoDeentrada = _context.Asientos
                .Include(f => f.IdSalaNavigation)
                .Where(f => f.IdAsiento == id)
                .FirstOrDefault();

            return detalleAsientoDeentrada;
        }

        public async Task<Entradum?> VerDetalleEntrada(int id)
        {
            var entrada = await _context.Entrada
                .Include(e => e.IdFuncionNavigation) // Incluye la entidad Funcion
                    .ThenInclude(f => f.IdPeliculaNavigation) // Luego, incluye la Pelicula dentro de Funcion
                .Include(e => e.IdFuncionNavigation) // Vuelve a incluir Funcion para ThenInclude otra navegación
                    .ThenInclude(f => f.IdSalaNavigation)     // Luego, incluye la Sala dentro de Funcion
                .Include(e => e.IdAsientoNavigation) // Incluye la entidad Asiento
                .AsNoTracking() // Usa AsNoTracking si solo vas a leer los datos y no modificarlos
                .FirstOrDefaultAsync(e => e.IdEntrada == id); // Filtra por IdEntrada

            return entrada;
        }

        public Funcion? VerDetalleFuncion(int id)
        {
            var detalleFuncionParaEntrada = _context.Funcions
                .Include(f => f.IdSalaNavigation)
                .Include(f => f.IdPeliculaNavigation)
                .Where(f => f.IdFuncion == id)
                .FirstOrDefault();
            return detalleFuncionParaEntrada;
        }
    }
}
