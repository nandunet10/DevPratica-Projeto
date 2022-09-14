using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioNegocio _usuarioNegocio;
        
        public UsuarioController(IUsuarioNegocio usuarioNegocio)
        {
            _usuarioNegocio = usuarioNegocio;
        }

        [HttpGet()]
        public async Task<List<UsuarioModel>> Get()
        {
            return await _usuarioNegocio.ObterLista();
        }
        [HttpGet("{cpf}")]
        public async Task<UsuarioModel> Get([FromQuery] string cpf)
        {
            return await _usuarioNegocio.Obter(cpf);
        }
        [HttpPost()]
        public async Task Post([FromBody] UsuarioModel usuarioModel)
        {
            await _usuarioNegocio.Inserir(usuarioModel);
        }

        [HttpPut()]
        public async Task Put([FromBody] UsuarioModel usuarioModel)
        {
            await _usuarioNegocio.Alterar(usuarioModel);
        }
    }
}
