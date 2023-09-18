using Financiera.Helpers;
using Financiera.Models.DTOs;
using Financiera.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financiera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly CuentaService _cuentaService;

        public CuentaController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CuentaDTO>> GetAll()
        {
            var result = _cuentaService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(CuentaDTO item)
        {
            return Ok(await _cuentaService.SaveAccount(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetById(int id)
        {
            return Ok(await _cuentaService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutC(CuentaDTO item)
        {
            return Ok(await _cuentaService.EditAccount(item));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> DeleteById(int id)
        {
            return Ok(await _cuentaService.DeleteAccount(id));
        }
    }
}
