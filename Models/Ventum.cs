using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdCliente { get; set; }

    public DateTime? FechaVenta { get; set; } = DateTime.Now;//Fecha Actual

    public decimal? Total { get; set; }

    public virtual ICollection<DetalleVentum> DetalleVenta { get; set; } = new List<DetalleVentum>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
