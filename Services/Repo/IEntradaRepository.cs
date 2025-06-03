using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IEntradaRepository
    {
        List<Entradum> GetEntradas();
        List<Entradum> ListarPorFuncion(int idFuncion);
        List<Entradum> ListarPorAsiento(int idAsiento);
        List<Entradum> ListarPorEstado(string estado);

        Entradum? GetEntradaById(int id);
        void MarcarEntradaVendida(Entradum entrada);
        void ReservarEntrada(Entradum entrada);
        void AddEntrada(Entradum entrada);
        void UpdateEntrada(Entradum entrada);
        void DeleteEntrada(int id);

        Funcion? VerDetalleFuncion(int id);
        Asiento? VerDetalleAsiento(int id);

    }
}
