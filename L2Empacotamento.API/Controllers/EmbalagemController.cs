using Microsoft.AspNetCore.Mvc;
using L2Empacotamento.Application.DTOs;

namespace L2Empacotamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmbalagemController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] EmpacotarPedidoRequest request)
        {
            return Ok(new { mensagem = "Requisição recebida!", pedidos = request.Pedidos});
        }
    }
}
