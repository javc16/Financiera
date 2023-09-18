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
            return account.SaldoInicial+ movimientoDTO.Valor;            
        }

        public bool valdiTransaction(Movimiento movimiento, MovimientoDTO movimientoDTO, Cuenta account)
        {
            if (movimientoDTO.Valor>0) 
            {
                return true;
            }
            if (movimiento != null  && movimiento.Saldo>=Math.Abs(movimientoDTO.Valor))
            {
                return true;
            }
            else if (account.SaldoInicial>= Math.Abs(movimientoDTO.Valor)) 
            {
                return true;
            }
            return false;
        }

        public bool dailyLimit(IEnumerable<Movimiento> movimientos, float valor)
        {
            if (valor>0) 
            {
                return false;
            }
            float valorTotal = 0;
            foreach (var mov in movimientos) 
            {
                valorTotal = valorTotal + Math.Abs(mov.Valor);
            }

            if (valorTotal+Math.Abs(valor)>1000) 
            {
                return true;
            }

            return false;
        }
    }
}
