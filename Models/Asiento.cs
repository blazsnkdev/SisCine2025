using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Asiento
{
    public int IdAsiento { get; set; }

    public int IdSala { get; set; }

    public int Fila { get; set; }

    public int Numero { get; set; }

    public virtual ICollection<Entradum> Entrada { get; set; } = new List<Entradum>();

    public virtual Sala? IdSalaNavigation { get; set; }/* = null!;*/
}
