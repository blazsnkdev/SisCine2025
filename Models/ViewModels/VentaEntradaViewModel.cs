namespace SistemaCineMVC.Models.ViewModels
{
    public class VentaEntradaViewModel
    {
        public Ventum Venta { get; set; }
        public decimal PrecioTotal { get; set; }
        public List<Entradum> Entradas { get; set; } = new List<Entradum>();
    }
}
