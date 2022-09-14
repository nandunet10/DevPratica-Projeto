using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Locacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class LocacaoController : ControllerBase
    {
        private readonly ILocacoesNegocio _locacoesNegocio;

        public LocacaoController(ILocacoesNegocio locacoesNegocio)
        {
            _locacoesNegocio = locacoesNegocio;
        }

        [HttpGet()]
        public async Task<List<LocacoesModel>> Get()
        {
            return await _locacoesNegocio.ObterLista();
        }
        [HttpGet("{id}")]
        public async Task<LocacoesModel> Get([FromRoute] int id)
        {
            return await _locacoesNegocio.Obter(id);
        }
        [HttpPost()]
        public async Task Post([FromBody] LocacoesModel model)
        {
            await _locacoesNegocio.Inserir(model);
        }

        [HttpPut()]
        public async Task Put([FromBody] LocacoesModel model)
        {
            await _locacoesNegocio.Alterar(model);
        }
    }
}
