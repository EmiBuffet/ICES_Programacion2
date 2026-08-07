namespace FacturasFront.Models
{
    public class Facturas
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}