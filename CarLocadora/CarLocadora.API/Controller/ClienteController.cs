using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Cliente;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteNegocio _clienteNegocio;
        public ClienteController(IClienteNegocio cliente)
        {
            _clienteNegocio = cliente;
        }

        [HttpGet()]
        public List<ClienteModel> Get()
        {
            return _clienteNegocio.ObterLista();
        }
        [HttpPost()]
        public void Post([FromBody] ClienteModel cliente)
        {
            _clienteNegocio.Inserir(cliente);
        }

        [HttpPut()]
        public void Put([FromBody] ClienteModel cliente)
        {
            _clienteNegocio.Alterar(cliente);
        }
    }
}
