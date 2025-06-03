using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Pelicula
{
    public int IdPelicula { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Genero { get; set; }

    public int Duracion { get; set; }

    public string Clasificacion { get; set; } = null!;

    public string? Sinopsis { get; set; }

    public virtual ICollection<Funcion> Funcions { get; set; } = new List<Funcion>();
}
