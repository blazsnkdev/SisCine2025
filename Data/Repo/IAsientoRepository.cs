using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IAsientoRepository
    {
        List<Asiento> ListarAsiento();
        List<Asiento> ListarPorSala(int? idSala);







        Asiento ObtenerAsiento(int id);
        void AgregarAsiento(Asiento sala);
        void ActualizarAsiento(Asiento sala);
        void EliminarAsiento(int id);

    }
}
