using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.FormasDePagamento;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormasDePagamentoController : ControllerBase
    {
        private readonly IFormasDePagamentoNegocio _formasDePagamentoNegocio;
           public FormasDePagamentoController(IFormasDePagamentoNegocio formasDePagamentoNegocio)
        {
            _formasDePagamentoNegocio = formasDePagamentoNegocio;
        }

        [HttpGet()]
        public List<FormasDePagamentoModel> Get()
        {
            return _formasDePagamentoNegocio.ObterLista();
        }
        [HttpPost()]
        public void Post([FromBody] FormasDePagamentoModel formasDePagamentoModel)
        {
            _formasDePagamentoNegocio.Inserir(formasDePagamentoModel);
        }

        [HttpPut()]
        public void Put([FromBody] FormasDePagamentoModel formasDePagamentoModel)
        {
            _formasDePagamentoNegocio.Alterar(formasDePagamentoModel);
        }
    }
}
