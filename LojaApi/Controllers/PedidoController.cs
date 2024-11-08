using LojaApi.Models;
using LojaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

        public class PedidosController : ControllerBase
        {
        private readonly PedidoRepository _pedidoRepository;

        public PedidosController(PedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // listar pedidos do usuario
        [HttpGet("listar-por-usuario")]
        [SwaggerOperation]

        public async Task<IActionResult> BuscarPorUsuario(string usuario)
        {
            var pedido = await _pedidoRepository.BuscarPorUsuario(usuario);
            if (pedido == null)
            {
                return NotFound("Não existe Pedidos com o usuario informado");
            }
            return Ok(pedido);
        }

        [HttpGet("historico-pedidos")]
        public async Task<IActionResult> ListagemHistoricoPedidos()
        {
            var pedidos = await _pedidoRepository.ListarHistoricoPedidosDB();
            return Ok(pedidos);
        }
    }
}
