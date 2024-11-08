using LojaApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using LojaApi.Models;
using Swashbuckle.AspNetCore.Annotations;


namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutosController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutosController(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        //listar todos os produtos
        [HttpGet("listar-produtos")]
        public async Task<IActionResult> ListarProdutos()
        {
            var produtos = await _produtoRepository.ListarProdutoDB();
            return Ok(produtos);
        }

        // cadastrar um novo produto
        [HttpPost("cadastrar-produto")]
        public async Task<IActionResult> CadastrarProduto([FromBody] Produto produto)
        {
            var produtoId = await _produtoRepository.CadastrarProdutoDB(produto);
            return Ok(new { mensagem = "Produto cadastrado com sucesso", produtoId });
        }

        // atualizar informações de um livro
        [HttpPut("atualizar-produto/{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] Produto produtoAtualizado)
        {
            var produto = await _produtoRepository.AtualizarProdutoDB(id, produtoAtualizado);
            if (produto == null)
            {
                return NotFound(new { mensagem = "Produto não encontrado" });
            }
            return Ok(new { mensagem = "Produto atualizado com sucesso" });
        }
        //excluir um produto
        [HttpDelete("excluir-produto/{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            var produtoExcluido = await _produtoRepository.ExcluirProdutoDB(id);
            if (!produtoExcluido)
            {
                return NotFound(new { mensagem = "Produto não encontrado" });
            }
            return Ok(new { mensagem = "Produto excluído com sucesso" });
        }
        [HttpGet("buscar-produtos")]
        public async Task<IActionResult> BuscarProdutos([FromQuery] string? nome, [FromQuery] string? descricao, [FromQuery] decimal? preco)
        {
            var produtos = await _produtoRepository.BuscarProdutos(nome, descricao,preco);
            return Ok(produtos);
        }
    }
 
}
