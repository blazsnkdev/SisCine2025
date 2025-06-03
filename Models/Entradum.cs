using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Entradum
{
    public int IdEntrada { get; set; }

    public int IdFuncion { get; set; }

    public int IdAsiento { get; set; }

    public decimal? Precio { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Asiento IdAsientoNavigation { get; set; } = null!;

    public virtual Funcion IdFuncionNavigation { get; set; } = null!;
}
