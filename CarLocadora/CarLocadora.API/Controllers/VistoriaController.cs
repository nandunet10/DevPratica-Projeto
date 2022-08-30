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
        public List<VistoriasModel> Get()
        {
            return _vistoriasNegocio.ObterLista();
        }
        [HttpGet("{id}")]
        public VistoriasModel Get([FromQuery] int id)
        {
            return _vistoriasNegocio.Obter(id);
        }
        [HttpPost()]
        public void Post([FromBody] VistoriasModel model)
        {
            _vistoriasNegocio.Inserir(model);
        }

        [HttpPut()]
        public void Put([FromBody] VistoriasModel model)
        {
            _vistoriasNegocio.Alterar(model);
        }
    }
}
