using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Vistorias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class VistoriaController : ControllerBase
    {
        private readonly IVistoriasNegocio _vistoriasNegocio;

        public VistoriaController(IVistoriasNegocio vistoriasNegocio)
        {
            _vistoriasNegocio = vistoriasNegocio;
        }

        [HttpGet()]
        public async Task<List<VistoriasModel>> Get()
        {
            return await _vistoriasNegocio.ObterLista();
        }
        [HttpGet("{id}")]
        public async Task<VistoriasModel> Get([FromQuery] int id)
        {
            return await _vistoriasNegocio.Obter(id);
        }
        [HttpPost()]
        public async Task Post([FromBody] VistoriasModel model)
        {
            await _vistoriasNegocio.Inserir(model);
        }

        [HttpPut()]
        public async Task Put([FromBody] VistoriasModel model)
        {
            await _vistoriasNegocio.Alterar(model);
        }
    }
}
