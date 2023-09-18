namespace Financiera.Models.DTOs
{
    public class OutputMovimientoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaId { get; set; }
        public float Valor { get; set; }
        public float Saldo { get; set; }
        public static OutputMovimientoDTO FromModelToDTO(Movimiento movimiento)
        {
            return movimiento != null ? new OutputMovimientoDTO
            {
                Id = movimiento.Id,
                Fecha = movimiento.Fecha,
                CuentaId = movimiento.Cuenta.Id,
                Valor = movimiento.Valor,
                Saldo = movimiento.Saldo,
            } : new OutputMovimientoDTO();
        }

        public static IEnumerable<OutputMovimientoDTO> FromModelToDTO(IEnumerable<Movimiento> movimiento)
        {
            if (movimiento == null)
            {
                return new List<OutputMovimientoDTO>();
            }
            List<OutputMovimientoDTO> movimientoData = new List<OutputMovimientoDTO>();

            foreach (var item in movimiento)
            {
                movimientoData.Add(FromModelToDTO(item));
            }

            return movimientoData;
        }
    }
}
