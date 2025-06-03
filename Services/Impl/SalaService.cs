using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class SalaService : ISalaService
    {

        private readonly BdCine2025Context _context;
        public SalaService(BdCine2025Context context)
        {
            _context = context;
        }

        public void ActualizarSala(Sala sala)
        {
            _context.Salas.Update(sala);
            _context.SaveChanges();
        }

        public void AgregarSala(Sala sala)
        {
            _context.Salas.Add(sala);
            _context.SaveChanges();
        }

        public void EliminarSala(int id)
        {
            var sala = _context.Salas.FirstOrDefault(s => s.IdSala == id);
            _context.Salas.Remove(sala);
            _context.SaveChanges();
        }

        public List<Sala> ListaSalas()
        {
            var lista = _context.Salas.ToList();
            return lista;
        }

        public Sala ObtenerSala(int id)
        {
            var salaEncontrada = _context.Salas
                .Where(s => s.IdSala == id)
                .FirstOrDefault();

            return salaEncontrada;
        }
    }
}
