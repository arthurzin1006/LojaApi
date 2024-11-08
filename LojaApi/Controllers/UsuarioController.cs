using LojaApi.Models;
using LojaApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LojaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // cadastrar um novo usuario
        [HttpPost("cadastrar-usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            var usuarioId = await _usuarioRepository.CadastrarUsuarioDB(usuario);
            return Ok(new { mensagem = "Usuario cadastrado com sucesso", usuarioId });
        }

        //listar todos os usuarios
        [HttpGet("listar-usuarios")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioRepository.ListarUsuarioDB();
            return Ok(usuarios);
        }
    }
}
