using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Veiculo;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoNegocio _veiculoNegocio;

        public VeiculoController(IVeiculoNegocio veiculoNegocio)
        {
            _veiculoNegocio = veiculoNegocio;
        }

        [HttpGet()]
        public List<VeiculoModel> Get()
        {
            return _veiculoNegocio.ObterLista();
        }

        [HttpGet("ObterDados")]
        public VeiculoModel Get([FromQuery] string placa)
        {
            return _veiculoNegocio.Obter(placa);
        }

        [HttpPost()]
        public void Post([FromBody] VeiculoModel veiculoModel)
        {
            _veiculoNegocio.Inserir(veiculoModel);
        }

        [HttpPut()]
        public void Put([FromBody] VeiculoModel veiculoModel)
        {
            _veiculoNegocio.Alterar(veiculoModel);
        }
    }
}
