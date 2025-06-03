using System.ComponentModel.DataAnnotations;

namespace SistemaCineMVC.Models.ViewModels
{
    public class ClienteViewModel
    {

        
        public int IdCliente { get; set; }
        
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Telefono { get; set; } = null!;
    }
}
