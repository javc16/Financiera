using Financiera.Helpers;
using Financiera.Models;
using Financiera.Models.DTOs;
using Financiera.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financiera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> GetAll()
        {
            var result = _clienteService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetById(int id)
        {
            return Ok(await _clienteService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post(ClienteDTO item)
        {
            return Ok(await _clienteService.SaveCliente(item));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> PutC(ClienteDTO item)
        {
            return Ok(await _clienteService.EditClient(item));
        }


        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteById(int id)
        {
            return Ok(await _clienteService.DeleteClient(id));
        }
    }
}
