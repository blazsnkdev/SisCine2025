using SistemaCineMVC.Data.Repo;
using SistemaCineMVC.Models;

namespace SistemaCineMVC.Data.Impl
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Cliente> Cliente { get; }

        public IRepository<Pelicula> Pelicula { get; }

        public IRepository<DetalleVentum> DetalleVenta { get; }

        public IRepository<Funcion> Funcion { get; }



        private readonly BdCine2025Context _context;
        public UnitOfWork(BdCine2025Context context,
            IRepository<Cliente> cliente,
            IRepository<Pelicula> pelicula,
            IRepository<DetalleVentum> detalleVenta,
            IRepository<Funcion> funcion
            )
        {
            _context = context;
            Cliente = cliente;
            Pelicula = pelicula;
            DetalleVenta = detalleVenta;
            Funcion = funcion;
        }



        public async Task<int> SaveChangesAsAsync(){
            return await _context.SaveChangesAsync();
        }
    }
}
