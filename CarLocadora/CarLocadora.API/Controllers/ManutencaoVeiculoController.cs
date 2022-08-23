using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.ManutencaoVeiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarLocadora.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ManutencaoVeiculoController : ControllerBase
    {
        private readonly IManutencaoVeiculoNegocio _manutencaoVeiculoNegocio;

        public ManutencaoVeiculoController(IManutencaoVeiculoNegocio manutencaoVeiculoNegocio)
        {
            _manutencaoVeiculoNegocio = manutencaoVeiculoNegocio;
        }

        // GET: api/<ManutencaoVeiculoController>
        [HttpGet()]
        public List<ManutencaoVeiculoModel> Get()
        {
            return _manutencaoVeiculoNegocio.ObterLista();
        }

        // GET api/<ManutencaoVeiculoController>/5
        [HttpGet("{id}")]
        public ManutencaoVeiculoModel Get([FromQuery] int id)
        {
            return _manutencaoVeiculoNegocio.Obter(id);

        }

        // POST api/<ManutencaoVeiculoController>
        [HttpPost]
        public void Post([FromBody] ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            _manutencaoVeiculoNegocio.Inserir(manutencaoVeiculoModel);
        }

        // PUT api/<ManutencaoVeiculoController>/5
        [HttpPut]
        public void Put([FromBody] ManutencaoVeiculoModel manutencaoVeiculoModel)
        {
            _manutencaoVeiculoNegocio.Alterar(manutencaoVeiculoModel);
        }

        // DELETE api/<ManutencaoVeiculoController>/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery] int id)
        {
            _manutencaoVeiculoNegocio.Excluir(id);
        }
    }
}
