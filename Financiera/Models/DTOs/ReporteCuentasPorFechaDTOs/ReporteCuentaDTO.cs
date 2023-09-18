namespace Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs
{
    public class ReporteCuentaDTO
    {
        public int NumeroDeCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public float SaldoInicial { get; set; }
        public bool EstadoCuenta { get; set; }
        public List<ReporteMovimientosDTO>? MovimientosCuenta { get; set; }

        public static ReporteCuentaDTO FromModelToDTO(Cuenta cuenta)
        {
            return cuenta != null ? new ReporteCuentaDTO
            {
               NumeroDeCuenta = cuenta.NumeroCuenta,
               EstadoCuenta = cuenta.Estado,
               SaldoInicial = cuenta.SaldoInicial,
               TipoCuenta = cuenta.TipoCuenta.Tipo,
            } : new ReporteCuentaDTO();
        }

        public static IEnumerable<ReporteCuentaDTO> FromModelToDTO(IEnumerable<Cuenta> cuenta)
        {
            if (cuenta == null)
            {
                return new List<ReporteCuentaDTO>();
            }
            List<ReporteCuentaDTO> cuentaData = new List<ReporteCuentaDTO>();

            foreach (var item in cuenta)
            {
                cuentaData.Add(FromModelToDTO(item));
            }

            return cuentaData;
        }
    }
}
