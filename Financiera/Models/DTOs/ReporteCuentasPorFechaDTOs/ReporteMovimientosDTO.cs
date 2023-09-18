namespace Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs
{
    public class ReporteMovimientosDTO
    {
        public DateTime Fecha { get; set; }
        public float Valor { get; set; }
        public float SaldoDisponible { get; set; }
    }
}
