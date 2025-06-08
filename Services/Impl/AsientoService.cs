using SistemaCineMVC.Models;
using SistemaCineMVC.Services.Interfaces;
using SistemaCineMVC.Services.Repo;

namespace SistemaCineMVC.Services.Impl
{
    public class AsientoService : IAsientoService
    {
        private readonly IAsientoRepository _asientoRepository;
        public AsientoService(IAsientoRepository asientoRepository)
        {
            _asientoRepository = asientoRepository;
        }

        public void Agregar(Asiento asiento)
        {
            bool asientoExiste = AsientoExiste(asiento.IdSala, asiento.Fila, asiento.Numero);
            if (asientoExiste)
            {
                throw new ArgumentException("El asiento ya existe en la sala especificada.");
            }
            _asientoRepository.AgregarAsiento(asiento);

        }



        public bool AsientoExiste(int idSala, int fila, int numero)
        {
            return _asientoRepository.ListarAsiento()
                .Any(a => a.IdSala == idSala && a.Fila == fila && a.Numero == numero);
        }
    }
}
