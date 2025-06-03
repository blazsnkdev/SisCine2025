using System;
using System.Collections.Generic;

namespace SistemaCineMVC.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
