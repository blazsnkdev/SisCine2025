using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IEntradaRepository
    {
        List<Entradum> GetEntradas();
        List<Entradum> ListarPorFuncion(int? idFuncion);
        List<Entradum> ListarPorAsiento(int? idAsiento);
        List<Entradum> ListarPorEstado(string estado);





        Entradum? GetEntradaById(int id);



        Task MarcarEntradaVendida(int id);
        Task MarcarCancelada(int id);




        void ReservarEntrada(Entradum entrada);
        void AddEntrada(Entradum entrada);
        void UpdateEntrada(Entradum entrada);
        void DeleteEntrada(int id);

        Task<Entradum?> VerDetalleEntrada(int id);
        Funcion? VerDetalleFuncion(int id);
        Asiento? VerDetalleAsiento(int id);

    }
}
