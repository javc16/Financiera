namespace Financiera.Models.DTOs
{
    public class MovimientoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaId { get; set; }
        public float Valor { get; set; }

        public static MovimientoDTO FromModelToDTO(Movimiento movimiento)
        {
            return movimiento != null ? new MovimientoDTO
            {
                Id = movimiento.Id,
                Fecha = movimiento.Fecha,
                CuentaId = movimiento.Cuenta.Id,
                Valor = movimiento.Valor,                                
            } : new MovimientoDTO();
        }

        public static IEnumerable<MovimientoDTO> FromModelToDTO(IEnumerable<Movimiento> movimiento)
        {
            if (movimiento == null)
            {
                return new List<MovimientoDTO>();
            }
            List<MovimientoDTO> movimientoData = new List<MovimientoDTO>();

            foreach (var item in movimiento)
            {
                movimientoData.Add(FromModelToDTO(item));
            }

            return movimientoData;
        }
    }
}
