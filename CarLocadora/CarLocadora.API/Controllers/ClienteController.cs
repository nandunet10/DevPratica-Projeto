using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    /// <summary>
    /// Operações com os catálogos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ClienteController : ControllerBase
    {
        private readonly IClienteNegocio _clienteNegocio;
        public ClienteController(IClienteNegocio clienteNegocio)
        {
            _clienteNegocio = clienteNegocio;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<List<ClienteModel>> Get()
        {
            return await _clienteNegocio.ObterLista();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterListaEnviarEmail")]
        public async Task<List<ClienteModel>> GetObterListaEnviarEmail()
        {
            return await _clienteNegocio.ObterListaEnviarEmail();
        }

        /// <summary>
        /// Método que obtém um Cliente
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpGet("{cpf}")]
        public async Task<ClienteModel> Get([FromQuery] string cpf)
        {
            return await _clienteNegocio.ObterAsync(cpf);
        }

        /// <summary>
        /// Método que cadastra um cliente e publica uma mensagem no rabbitMQ
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task Post([FromBody] ClienteModel clienteModel)
        {
            await _clienteNegocio.Inserir(clienteModel);
        }

        /// <summary>
        /// Método que altera um cliente
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        [HttpPut()]
        public async Task Put([FromBody] ClienteModel clienteModel)
        {
            await _clienteNegocio.Alterar(clienteModel);

        }

        /// <summary>
        /// Método que altera um cliente após o envio de e-mail
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        [HttpPut("AlterarEnvioDeEmail")]
        public async Task PutAlterarEnvioDeEmail([FromBody] string cpf)
        {
            await _clienteNegocio.AlterarEnvioDeEmail(cpf);
        }
    }
}
