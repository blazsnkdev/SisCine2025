﻿using SistemaCineMVC.Models;

namespace SistemaCineMVC.Services.Repo
{
    public interface IPeliculaRepository
    {
        List<Pelicula> ListaPeliculas();
        void AgregarPelicula(Pelicula pelicula);
        Pelicula ObtenerPelicula(int id);
        void ActualizarPelicula(Pelicula pelicula);
        void EliminarPelicula(int id);



    }
}
