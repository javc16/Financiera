using AutoMapper;
using Financiera.Constants;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Helpers;
using Financiera.Models;
using Financiera.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Financiera.Services
{
    public class ReporteService
    {
        private readonly IRepository<Movimiento> _movimientoContext;
        private readonly IRepository<Cuenta> _accountContext;
        private readonly IRepository<Cliente> _clientContext;
        private readonly IMapper _mapper;

        public ReporteService(IRepository<Movimiento> movimientoContext, IMapper mapper,  IRepository<Cuenta> accountContext, IRepository<Cliente> clientContext)
        {
            _movimientoContext = movimientoContext;
            _mapper = mapper;
            _accountContext = accountContext;
            _clientContext = clientContext;
        }

        public async Task<Response> GetByDate(int id)
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


    }
}
