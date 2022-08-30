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
        public List<LocacoesModel> Get()
        {
            return _locacoesNegocio.ObterLista();
        }
        [HttpGet("{id}")]
        public LocacoesModel Get([FromQuery] int id)
        {
            return _locacoesNegocio.Obter(id);
        }
        [HttpPost()]
        public void Post([FromBody] LocacoesModel model)
        {
            _locacoesNegocio.Inserir(model);
        }

        [HttpPut()]
        public void Put([FromBody] LocacoesModel model)
        {
            _locacoesNegocio.Alterar(model);
        }
    }
}
