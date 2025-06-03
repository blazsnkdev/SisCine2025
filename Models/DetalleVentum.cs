using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class DetalleVentum
{
    public int IdDetalle { get; set; }

    public int IdVenta { get; set; }

    public int IdEntrada { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public virtual Entradum IdEntradaNavigation { get; set; } = null!;

    public virtual Ventum IdVentaNavigation { get; set; } = null!;
}
