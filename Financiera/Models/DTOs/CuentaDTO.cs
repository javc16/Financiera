namespace Financiera.Models.DTOs
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public int NumeroCuenta { get; set; }
        public int ClienteId { get; set; }
        public int TipoCuentaId { get; set; }
        public float SaldoInicial { get; set; }
        public bool Estado { get; set; }

        public static CuentaDTO FromModelToDTO(Cuenta cuenta)
        {
            return cuenta != null ? new CuentaDTO
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                TipoCuentaId = cuenta.TipoCuenta.Id,
                ClienteId = cuenta.Cliente.Id,
                Estado = cuenta.Estado
            } : new CuentaDTO();
        }

        public static IEnumerable<CuentaDTO> FromModelToDTO(IEnumerable<Cuenta> cuenta)
        {
            if (cuenta == null)
            {
                return new List<CuentaDTO>();
            }
            List<CuentaDTO> cuentaData = new List<CuentaDTO>();

            foreach (var item in cuenta)
            {
                cuentaData.Add(FromModelToDTO(item));
            }

            return cuentaData;
        }
    }
}
