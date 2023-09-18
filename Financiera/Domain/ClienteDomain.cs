using Financiera.Helpers;
using Financiera.Models;
using Financiera.Models.DTOs;

namespace Financiera.Domain
{
    public class ClienteDomain
    {
        public bool DuplicateCliente(Cliente cliente, ClienteDTO clienteDTO) 
        {
            if (cliente != null && cliente.Identificacion.Equals(clienteDTO.Identificacion))
            {
               return true;
            }
            return false;
        }

        
      
    }
}
