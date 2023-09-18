namespace Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs
{
    public class ReporteCuentaDTO
    {
        public string? NumeroDeCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public float SaldoInicial { get; set; }
        public bool EstadoCuenta { get; set; }
        List<ReporteMovimientosDTO>? MovimientosCuenta { get; set; }
    }
}
