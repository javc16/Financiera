using Financiera.Helpers;
using Financiera.Models;
using Financiera.Models.DTOs;

namespace Financiera.Domain
{
    public class ClienteDomain
    {
        public bool DuplicateCliente(Cliente cliente) 
        {
            if (cliente != null)
            {
               return true;
            }
            return false;
        }
      
    }
}
