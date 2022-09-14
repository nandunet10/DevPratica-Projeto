using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.Categoria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarLocadora.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaNegocio _categoriaNegocio;

        public CategoriaController(ICategoriaNegocio categoriaNegocio)
        {
            _categoriaNegocio = categoriaNegocio;
        }

        [HttpGet()]
        public async Task<List<CategoriaModel>> GetAsync()
        {
            return await _categoriaNegocio.ObterLista();
        }
        [HttpGet("{id}")]
        public async Task<CategoriaModel> GetAsync([FromQuery] int id)
        {
            return await _categoriaNegocio.Obter(id);
        }
        [HttpPost()]
        public async Task Post([FromBody] CategoriaModel categoriaModel)
        {
            await _categoriaNegocio.Inserir(categoriaModel);
        }

        [HttpPut()]
        public async Task Put([FromBody] CategoriaModel categoriaModel)
        {
            await _categoriaNegocio.Alterar(categoriaModel);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromQuery] int id)
        {
            await _categoriaNegocio.Excluir(id);
        }
    }
}
