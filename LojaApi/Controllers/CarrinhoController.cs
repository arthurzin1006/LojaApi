using LojaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using LojaApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

        public class CarrinhosController : ControllerBase
        {
            private readonly CarrinhoRepository _carrinhoRepository;

            public CarrinhosController(CarrinhoRepository carrinhoRepository)
            {
                _carrinhoRepository = carrinhoRepository;
            }

        //listar todo o carrinho
        [HttpGet("listar-carrinho")]
        public async Task<IActionResult> ListarCarrinhos()
        {
            var carrinhos = await _carrinhoRepository.ListarCarrinhoDB();
            return Ok(carrinhos);
        }

        // cadastrar um novo produto ao carrinho
        [HttpPost("cadastrar-produto-ao-carrinho")]
        public async Task<IActionResult> CadastrarCarrinho([FromBody] Carrinho carrinho)
        {
            var carrinhoId = await _carrinhoRepository.CadastrarCarrinhoDB(carrinho);
            return Ok(new { mensagem = "Produto adicionado ao carrinho com sucesso", carrinhoId });
        }
        //excluir do carrinho
        [HttpDelete("excluir-do-carrinho/{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            var carrinhoExcluido = await _carrinhoRepository.ExcluirCarrinhoDB(id);
            if (!carrinhoExcluido)
            {
                return NotFound(new { mensagem = "Não encontrado" });
            }
            return Ok(new { mensagem = "Excluído com sucesso" });
        }
    }
}
