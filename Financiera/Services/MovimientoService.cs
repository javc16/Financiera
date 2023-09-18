using AutoMapper;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Models.DTOs;
using Financiera.Models;
using Financiera.Constants;
using Financiera.Helpers;

namespace Financiera.Services
{
    public class MovimientoService
    {
        private readonly IRepository<Movimiento> _context;
        private readonly IRepository<Cuenta> _accountcontext;

        private readonly MovimientoDomain _movimientoDomain;
        private readonly IMapper _mapper;

        public MovimientoService(IRepository<Movimiento> context, IMapper mapper, MovimientoDomain movimientoDomain, IRepository<Cuenta> accountcontext)
        {
            _context = context;
            _mapper = mapper;
            _movimientoDomain = movimientoDomain;
            _accountcontext = accountcontext;
        }

        public IEnumerable<MovimientoDTO> GetAll()
        {
            var movimientos = _context.Find(x=>x.Id>0,X => X.Cuenta);
            var movimientosDTOs = MovimientoDTO.FromModelToDTO(movimientos);
            return movimientosDTOs;
        }

        public async Task<Response> SaveMovimiento(MovimientoDTO movimientoDTO)
        {
            var existingAccount = await _context.GetById(movimientoDTO.Id);

            var lastUpdate = _context.Find(x => x.Cuenta.Id == movimientoDTO.CuentaId).OrderBy(x => x.Fecha).LastOrDefault();
            var movimientoDeHoy = _context.Find(x => x.Cuenta.Id == movimientoDTO.CuentaId && x.Fecha > DateTime.Now.AddDays(-1));
            var movimiento = _mapper.Map<Movimiento>(movimientoDTO);
            var account = _accountcontext.Find(x => x.Id == movimientoDTO.CuentaId).FirstOrDefault();
            movimiento.Cuenta = account;

            var validTransaction = _movimientoDomain.valdiTransaction(movimiento, movimientoDTO, account);
            if (!validTransaction) 
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"Saldo no disponible",
                };
            }

            var dailyLimitReached = _movimientoDomain.dailyLimit(movimientoDeHoy,movimientoDTO.Valor);
            if (dailyLimitReached)
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"{Constantes.limitReached}",
                };
            }
            movimiento.Saldo = _movimientoDomain.returnSaldo(movimiento,movimientoDTO,account);

            _context.Add(movimiento);
            _context.SaveChanges();
            return new Response
            {
                Status = Constantes.Sucess,
                Message = $"{Constantes.This} {Constantes.Movimiento} {Constantes.SucefullyRegister}",
                Data = movimientoDTO
            };

        }

        public async Task<Response> GetById(int id)
        {
            var movimiento = await _context.GetById(id, x => x.Cuenta);
            if (movimiento == null)
            {
                return new Response
                {
                    Status = Constantes.Failed,
                    Message = $"{Constantes.This} {Constantes.Movimiento} con id: {id} {Constantes.DoesNotExist}"
                };
            }
            var movimientoDTO = MovimientoDTO.FromModelToDTO(movimiento);
            return new Response
            {
                Status = Constantes.Sucess,
                Message = "Recuperado Exitosamente",
                Data = movimientoDTO
            };
        }

        public async Task<Response> EditMovimiento(MovimientoDTO movimientoDTO)
        {
            var currentMovimientoDTO = await GetById(movimientoDTO.Id);
            if (currentMovimientoDTO.Data != null )
            {
                var currentMovimiento = _mapper.Map<Movimiento>(movimientoDTO);
                _context.Update(currentMovimiento);
                _context.SaveChanges();
                return new Response
                {
                    Status = Constantes.Sucess,
                    Message = $"Movimiento {movimientoDTO.Id} modificado correctamente!"
                };
            }
            return new Response
            {
                Status = Constantes.Failed,
                Message = $"Movimiento {movimientoDTO.Id} no existe!"
            };
        }

    }
}
