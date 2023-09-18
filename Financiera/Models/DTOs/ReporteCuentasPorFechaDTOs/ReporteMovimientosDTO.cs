namespace Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs
{
    public class ReporteMovimientosDTO
    {
        public DateTime Fecha { get; set; }
        public float Valor { get; set; }
        public float SaldoDisponible { get; set; }

        public static ReporteMovimientosDTO FromModelToDTO(Movimiento movimiento)
        {
            return movimiento != null ? new ReporteMovimientosDTO
            {
                Fecha = movimiento.Fecha,
                Valor = movimiento.Valor,
                SaldoDisponible = movimiento.Saldo
            } : new ReporteMovimientosDTO();
        }

        public static IEnumerable<ReporteMovimientosDTO> FromModelToDTO(IEnumerable<Movimiento> movimiento)
        {
            if (movimiento == null)
            {
                return new List<ReporteMovimientosDTO>();
            }
            List<ReporteMovimientosDTO> movimientoData = new List<ReporteMovimientosDTO>();

            foreach (var item in movimiento)
            {
                movimientoData.Add(FromModelToDTO(item));
            }

            return movimientoData;
        }
    }
}
