using AutoMapper;
using Financiera.Constants;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Helpers;
using Financiera.Models;
using Financiera.Models.DTOs;
using Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs;
using Microsoft.EntityFrameworkCore;

namespace Financiera.Services
{
    public class ReporteService
    {
        private readonly IRepository<Movimiento> _movimientoContext;
        private readonly IRepository<Cuenta> _accountContext;
        private readonly IRepository<Cliente> _clientContext;

        public ReporteService(IRepository<Movimiento> movimientoContext,IRepository<Cuenta> accountContext, IRepository<Cliente> clientContext)
        {
            _movimientoContext = movimientoContext;
            _accountContext = accountContext;
            _clientContext = clientContext;
        }

        public async Task<Response> GetByDateAndUser(DateTime fechaInicial, DateTime fechaFinal,int id)
        {
            var cliente = await _clientContext.GetById(id, x => x.Genero);
            var cuentas = _accountContext.Find(x => x.Cliente.Id == id, x => x.TipoCuenta);
            var cuentasReporte = ReporteCuentaDTO.FromModelToDTO(cuentas).ToList();
            foreach (var cuenta in cuentasReporte) 
            {
                var movimientos = _movimientoContext.Find(x=>x.Cuenta.NumeroCuenta == cuenta.NumeroDeCuenta && x.Fecha>=fechaInicial && x.Fecha<=fechaFinal);
                var movimientosReporte = ReporteMovimientosDTO.FromModelToDTO(movimientos).ToList();
                cuenta.MovimientosCuenta = movimientosReporte;
            }
            var responseData = new ReporteCuentasClientePorFechaDTO()
            {
                NombreCliente = cliente.Nombre,
                Estado = cliente.Estado,
                Cuentas = cuentasReporte,
            };
            return new Response
            {
                Status = Constantes.Sucess,
                Message = "Recuperado Correctamente",
                Data = responseData
            };
        }


    }
}
