using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Veiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoNegocio _veiculoNegocio;

        public VeiculoController(IVeiculoNegocio veiculoNegocio)
        {
            _veiculoNegocio = veiculoNegocio;
        }

        [HttpGet()]
        public async Task<List<VeiculoModel>> Get()
        {
            return await _veiculoNegocio.ObterLista();
        }

        [HttpGet("{placa}")]
        public async Task<VeiculoModel> Get([FromQuery] string placa)
        {
            return await _veiculoNegocio.Obter(placa);
        }

        [HttpPost()]
        public async Task Post([FromBody] VeiculoModel veiculoModel)
        {
            await _veiculoNegocio.Inserir(veiculoModel);
        }

        [HttpPut()]
        public async Task Put([FromBody] VeiculoModel veiculoModel)
        {
            await _veiculoNegocio.Alterar(veiculoModel);
        }
    }
}
