using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Categoria;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaNegocio _categoriaNegocio;

        public CategoriaController(ICategoriaNegocio categoriaNegocio)
        {
            _categoriaNegocio = categoriaNegocio;
        }

        [HttpGet()]
        public List<CategoriaModel> GetAll()
        {
            return _categoriaNegocio.ObterLista();
        }
        [HttpPost()]
        public void Post([FromBody] CategoriaModel categoria)
        {
            _categoriaNegocio.Inserir(categoria);
        }

        [HttpPut()]
        public void Put([FromBody] CategoriaModel categoria)
        {
            _categoriaNegocio.Alterar(categoria);
        }
        [HttpDelete()]
        public void Delete([FromQuery] int id)
        {
            _categoriaNegocio.Excluir(id);
        }
    }
}
