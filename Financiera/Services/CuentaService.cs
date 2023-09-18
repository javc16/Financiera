using AutoMapper;
using Financiera.Constants;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Models.DTOs;
using Financiera.Models;
using Financiera.Helpers;

namespace Financiera.Services
{
    public class CuentaService
    {
        private readonly IRepository<Cuenta> _context;
        private readonly IRepository<TipoCuenta> _ContextTipoCuenta;
        private readonly IRepository<Cliente> _clientContext;

        private readonly CuentaDomain _cuentaDomain;
        private readonly IMapper _mapper;

        public CuentaService(IRepository<Cuenta> context, IRepository<TipoCuenta> contextTipoCuenta, IMapper mapper, CuentaDomain cuentaDomain, IRepository<Cliente> clientContext)
        {
            _context = context;
            _ContextTipoCuenta = contextTipoCuenta;
            _mapper = mapper;
            _cuentaDomain = cuentaDomain;
            _clientContext = clientContext;
        }

        public IEnumerable<CuentaDTO> GetAll()
        {
            var cuentas = _context.Find(x => x.Estado == true, x => x.TipoCuenta, x => x.Cliente);
            var cuentasDTOs = CuentaDTO.FromModelToDTO(cuentas);
            return cuentasDTOs;
        }

        public async Task<Response> GetById(int id)
        {
            var cuenta = await _context.GetById(id, x => x.TipoCuenta, x=>x.Cliente);
            if (cuenta == null)
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"{Constantes.This} {Constantes.Cuenta} con id: {id} {Constantes.DoesNotExist}"
                };
            }
            var cuentaDTO = CuentaDTO.FromModelToDTO(cuenta);
            return new Response
            {
                Status = Constantes.Sucess,
                Message = "Recuperado Exitosamente",
                Data = cuentaDTO
            };
        }
    
        public async Task<Response> SaveAccount(CuentaDTO accountDTO)
        {
            var accounts = _context.GetAll();
            var existingAccount = accounts.FirstOrDefault(c => c.NumeroCuenta == accountDTO.NumeroCuenta);
            if (_cuentaDomain.DuplicateAccount(existingAccount))
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"{Constantes.This} {Constantes.Cuenta} con numero de cuenta:{accountDTO.NumeroCuenta} {Constantes.Duplicated}"
                };
            }
            
            var account = _mapper.Map<Cuenta>(accountDTO);
            var client = _clientContext.Find(x => x.Id == accountDTO.ClienteId).FirstOrDefault();
            var accountType = _ContextTipoCuenta.Find(x => x.Id == accountDTO.TipoCuentaId).FirstOrDefault();
            account.Cliente = client;
            account.TipoCuenta = accountType;
            _context.Add(account);
            _context.SaveChanges();
            return new Response
            {
                Status = Constantes.Sucess,
                Message = $"{Constantes.This} {Constantes.Cuenta} {Constantes.SucefullyRegister}",
                Data = accountDTO
            };
            
        }
        public async Task<Response> EditAccount(CuentaDTO accountDTO)
        {
            var currentAccountDTO = await GetById(accountDTO.Id);
            if (currentAccountDTO.Data != null)
            {
                var currentAccount = _mapper.Map<Cuenta>(accountDTO);
                _context.Update(currentAccount);
                _context.SaveChanges();
                return new Response
                {
                    Status = Constantes.Sucess,
                    Message = $"Cuenta {accountDTO.NumeroCuenta} modificada exitosamente!"
                };
            }
            return new Response
            {
                Status = Constantes.Failed,
                Message = $"Cuenta {accountDTO.NumeroCuenta} no existe!"
            };
        }

        public async Task<Response> DeleteAccount(int id)
        {
            var currentAccountDTO = await GetById(id);
            if (currentAccountDTO.Data != null)
            {
                var currentAccount = _mapper.Map<Cuenta>(currentAccountDTO.Data);

                currentAccount.Estado = false;
                _context.Update(currentAccount);
                _context.SaveChanges();
                return new Response
                {
                    Status = Constantes.Sucess,
                    Message = $"Cuenta {currentAccount.NumeroCuenta} borrado correctamente!"
                };
            }
            return new Response
            {
                Status = Constantes.Failed,
                Message = $"Cuenta {Constantes.DoesNotExist}!"
            };
        }
    }
}
