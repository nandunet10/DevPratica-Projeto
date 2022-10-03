using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class ClienteController : ControllerBase
    {
        private readonly IClienteNegocio _clienteNegocio;
        public ClienteController(IClienteNegocio clienteNegocio)
        {
            _clienteNegocio = clienteNegocio;
        }

        [HttpGet()]
        public async Task<List<ClienteModel>> Get()
        {
            return await _clienteNegocio.ObterLista();
        }

        [HttpGet("ObterListaEnviarEmail")]
        public async Task<List<ClienteModel>> GetObterListaEnviarEmail()
        {
            return await _clienteNegocio.ObterListaEnviarEmail();
        }
        [HttpGet("{cpf}")]
        public async Task<ClienteModel> Get([FromQuery] string cpf)
        {
            return await _clienteNegocio.ObterAsync(cpf);
        }
        [HttpPost()]
        public async Task Post([FromBody] ClienteModel clienteModel)
        {
            await _clienteNegocio.Inserir(clienteModel);
        }

        [HttpPut()]
        public async Task Put([FromBody] ClienteModel clienteModel)
        {
            await _clienteNegocio.Alterar(clienteModel);
        }
    }
}
