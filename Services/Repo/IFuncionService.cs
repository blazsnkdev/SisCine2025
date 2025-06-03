using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IFuncionService
    {
        List<Funcion> Lista();
       
        void Agregar(Funcion funcion);
        void Actualizar(Funcion funcion);
        void Eliminar(int id);
        //List<Funcion> FuncionesParaPelicula(string pelicula);
        Funcion ObtenerFuncion(int id);
    }
}
