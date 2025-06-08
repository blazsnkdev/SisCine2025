using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly BdCine2025Context _context;
        public PeliculaRepository(BdCine2025Context context)
        {
            _context = context;
        }
        public void ActualizarPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Update(pelicula);
            _context.SaveChanges();
        }

        public void AgregarPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            _context.SaveChanges();
        }

        public void EliminarPelicula(int id)
        {
            var pelicula = _context.Peliculas.FirstOrDefault(c => c.IdPelicula == id);
           
                _context.Peliculas.Remove(pelicula);
                _context.SaveChanges();
           

        }

        public List<Pelicula> ListaPeliculas()
        {
            var lista = _context.Peliculas.ToList();
            return lista;
        }

        public Pelicula ObtenerPelicula(int id)
        {
            var pelicula = _context.Peliculas.FirstOrDefault(c => c.IdPelicula == id);
        
            return pelicula;//puede retonar null pero en mi IAction result hago la validacion
        }
    }
}
