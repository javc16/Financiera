using Financiera.Models;

namespace Financiera.Domain
{
    public class CuentaDomain
    {
        public bool DuplicateAccount(Cuenta cuenta)
        {
            if (cuenta != null)
            {
                return true;
            }
            return false;
        }
    }
}
