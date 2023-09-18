using Financiera.Helpers;
using Financiera.Models.DTOs;
using Financiera.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financiera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly MovimientoService _movimientoService;

        public MovimientoController(MovimientoService movimientoService)
        {
            _movimientoService = movimientoService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<MovimientoDTO>> GetAll()
        {
            var result = _movimientoService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(MovimientoDTO item)
        {
            return Ok(await _movimientoService.SaveMovimiento(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetById(int id)
        {
            return Ok(await _movimientoService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutC(MovimientoDTO item)
        {
            return Ok(await _movimientoService.EditMovimiento(item));
        }
    }
}
