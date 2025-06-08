using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaCineMVC.Services.Impl
{
    public class FuncionRepository : IFuncionRepository
    {

        private readonly BdCine2025Context _context;
        public FuncionRepository(BdCine2025Context context)
        {
            _context = context;
        }
        public void Actualizar(Funcion funcion)
        {
            _context.Funcions.Update(funcion);
            _context.SaveChanges();
        }

        public void Agregar(Funcion funcion)
        {
            _context.Funcions.Add(funcion);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var funcion = _context.Funcions.FirstOrDefault(c => c.IdFuncion == id);
            _context.Funcions.Remove(funcion);
            _context.SaveChanges();

        }

        //public List<Funcion> FuncionesParaPelicula(string tituloPelicula)
        //{
        //    tituloPelicula = _context.Peliculas
        //        .Where(p => p.Titulo == tituloPelicula)
        //        .Select(p => p.IdPelicula)
        //        .FirstOrDefault()
        //        .ToString();

        //    var lista = _context.Funcions
        //        .Include(f => f.IdPeliculaNavigation)
        //        .Include(f => f.IdSalaNavigation)
        //        .Where(f => f.IdPeliculaNavigation.Titulo == tituloPelicula)
        //        .ToList();
        //    return lista;
        //}

        public List<Funcion> Lista()
        {
            var lista = _context.Funcions
                .Include(f => f.IdPeliculaNavigation)
                .Include(f => f.IdSalaNavigation)
                .ToList();

            return lista;
        }

        public Funcion ObtenerFuncion(int id)
        {
            var funcion = _context.Funcions.FirstOrDefault(f => f.IdFuncion == id);
            return funcion;
        }



        //public List<SelectListItem> Peliculas()
        //{
        //    var peliculas =_context.Peliculas.ToList();
        //    return _context.Peliculas
        //       .Select(p => new SelectListItem { Value = p.IdPelicula.ToString(), Text = p.Titulo })
        //       .ToList();

        //}

        //public List<SelectList> Salas()
        //{
        //    var salas = new SelectList(_context.Salas,"IdSala","Nombre");
        //    return salas;
        //}


    }
}
