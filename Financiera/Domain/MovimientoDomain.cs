using Financiera.Models;
using Financiera.Models.DTOs;
using Microsoft.Identity.Client;

namespace Financiera.Domain
{
    public class MovimientoDomain
    {
        public float returnSaldo(Movimiento movimiento, MovimientoDTO movimientoDTO, Cuenta account) 
        {
            if (movimiento != null)
            {
                return movimiento.Saldo + movimientoDTO.Valor;
            }
            return account.SaldoInicial;            
        }

        public bool valdiTransaction(Movimiento movimiento, MovimientoDTO movimientoDTO, Cuenta account)
        {
            if (movimiento != null && movimiento.Saldo> movimientoDTO.Valor)
            {
                return true;
            }
            else if (account.SaldoInicial> movimientoDTO.Valor) 
            {
                return true;
            }
            return false;
        }

        public bool dailyLimit(IEnumerable<Movimiento> movimientos, float valor)
        {
             float valorTotal = 0;
            foreach (var mov in movimientos) 
            {
                valorTotal = valorTotal + mov.Valor;
            }

            if (valorTotal+valor>1000) 
            {
                return true;
            }

            return false;
        }
    }
}
