using L2Empacotamento.Application.DTOs;
using L2Empacotamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace L2Empacotamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmbalagemController : ControllerBase
    {
        private readonly IEmbalagemService _embalagemService;
        public EmbalagemController(IEmbalagemService embalagemService)
        {
            _embalagemService = embalagemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpacotarPedidoRequest request)
        {
            if(request == null)
                return BadRequest("Pedido Inválido.");

            var response = await _embalagemService.EmpacotarAsync(request);

            return Ok(response);
        }
    }
}
