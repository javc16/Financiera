namespace Financiera.Models
{
    public class Movimiento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public Cuenta Cuenta { get; set; }
        public float Valor { get; set; }
        public float Saldo { get; set; }
    }
}
