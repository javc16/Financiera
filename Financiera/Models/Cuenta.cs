using System.ComponentModel.DataAnnotations;

namespace Financiera.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int NumeroCuenta { get; set; }
        public Cliente Cliente { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public float SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public List<Movimiento>? Movimientos { get; set; }

    }
}
