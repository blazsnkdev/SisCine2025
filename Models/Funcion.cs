using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Funcion
{
    public int IdFuncion { get; set; }

    public int IdPelicula { get; set; }

    public int IdSala { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual Pelicula? IdPeliculaNavigation { get; set; }/* = null!;*/

    public virtual Sala? IdSalaNavigation { get; set; }/* = null!;*/
}
