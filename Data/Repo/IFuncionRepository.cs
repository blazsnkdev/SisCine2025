using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IFuncionRepository
    {
        List<Funcion> Lista();
       
        void Agregar(Funcion funcion);
        void Actualizar(Funcion funcion);
        void Eliminar(int id);
        
        Funcion ObtenerFuncion(int id);
    }
}
