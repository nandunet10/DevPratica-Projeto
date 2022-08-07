using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioNegocio _usuarioNegocio;
        
        public UsuarioController(IUsuarioNegocio usuarioNegocio)
        {
            _usuarioNegocio = usuarioNegocio;
        }

        [HttpGet()]
        public List<UsuarioModel> Get()
        {
            return _usuarioNegocio.ObterLista();
        }
        [HttpGet("ObterDados")]
        public UsuarioModel Get([FromQuery] string cpf)
        {
            return _usuarioNegocio.Obter(cpf);
        }
        [HttpPost()]
        public void Post([FromBody] UsuarioModel usuarioModel)
        {
            _usuarioNegocio.Inserir(usuarioModel);
        }

        [HttpPut()]
        public void Put([FromBody] UsuarioModel usuarioModel)
        {
            _usuarioNegocio.Alterar(usuarioModel);
        }
    }
}
