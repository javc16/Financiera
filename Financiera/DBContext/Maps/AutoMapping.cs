using AutoMapper;
using Financiera.Models;
using Financiera.Models.DTOs;

namespace Financiera.DBContext.Maps
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Cliente, ClienteDTO>();
            CreateMap<ClienteDTO, Cliente>();
            CreateMap<Cuenta, CuentaDTO>();
            CreateMap<CuentaDTO, Cuenta>();
            CreateMap<Movimiento, MovimientoDTO>();
            CreateMap<MovimientoDTO, Movimiento>();
        }
    
    }
}
