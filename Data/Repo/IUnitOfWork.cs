using SistemaCineMVC.Models;

namespace SistemaCineMVC.Data.Repo
{
    public interface IUnitOfWork
    {
        IRepository<Cliente> Cliente { get; }
        IRepository<Pelicula> Pelicula { get; }
        IRepository<DetalleVentum> DetalleVenta { get; }
        IRepository<Funcion> Funcion { get; }






        Task<int> SaveChangesAsAsync();//esto siempre va estar

    }
}
