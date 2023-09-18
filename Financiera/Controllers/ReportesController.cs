using Financiera.Helpers;
using Financiera.Models.DTOs;
using Financiera.Models.DTOs.ReporteCuentasPorFechaDTOs;
using Financiera.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financiera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReportesController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetById(DateTime fechaInicial, DateTime fechaFinal,int id)
        {
            return Ok(await _reporteService.GetByDateAndUser(fechaInicial, fechaFinal,id));
        }
    }
}
