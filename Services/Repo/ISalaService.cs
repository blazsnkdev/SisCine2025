using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface ISalaService
    {

        List<Sala> ListaSalas();

        void AgregarSala(Sala sala);
        void ActualizarSala(Sala sala);
        Sala ObtenerSala(int id);
        void EliminarSala(int id);


    }
}
