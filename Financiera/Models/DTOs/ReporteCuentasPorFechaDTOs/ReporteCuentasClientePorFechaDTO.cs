namespace Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs
{
    public class ReporteCuentasClientePorFechaDTO
    {
        public string? NombreCliente { get; set; }
        public bool  Estado { get; set; }
        public List<ReporteCuentaDTO>? Cuentas { get; set; }
    }
}
