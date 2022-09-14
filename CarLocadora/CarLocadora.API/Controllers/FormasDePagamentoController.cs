using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.FormasDePagamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class FormasDePagamentoController : ControllerBase
    {
        private readonly IFormasDePagamentoNegocio _formasDePagamentoNegocio;
        public FormasDePagamentoController(IFormasDePagamentoNegocio formasDePagamentoNegocio)
        {
            _formasDePagamentoNegocio = formasDePagamentoNegocio;
        }

        [HttpGet()]
        public async Task<List<FormasDePagamentoModel>> Get()
        {
            return await _formasDePagamentoNegocio.ObterLista();
        }
        [HttpGet("{id}")]
        public async Task<FormasDePagamentoModel> Get([FromQuery] int id)
        {
            return await _formasDePagamentoNegocio.Obter(id);
        }
        [HttpPost()]
        public async Task Post([FromBody] FormasDePagamentoModel formasDePagamentoModel)
        {
            await _formasDePagamentoNegocio.Inserir(formasDePagamentoModel);
        }

        [HttpPut()]
        public async Task Put([FromBody] FormasDePagamentoModel formasDePagamentoModel)
        {
            await _formasDePagamentoNegocio.Alterar(formasDePagamentoModel);
        }
    }
}
