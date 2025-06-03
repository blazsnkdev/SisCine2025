using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Sala
{
    public int IdSala { get; set; }

    public string? Nombre { get; set; }

    public int Capacidad { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();

    public virtual ICollection<Funcion> Funcions { get; set; } = new List<Funcion>();
}
