﻿using CarLocadora.Modelo.Modelos;
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
        public List<CategoriaModel> Get()
        {
            return _categoriaNegocio.ObterLista();
        }
        [HttpGet("ObterDados")]
        public CategoriaModel Get([FromQuery] int id)
        {
            return _categoriaNegocio.Obter(id);
        }
        [HttpPost()]
        public void Post([FromBody] CategoriaModel categoriaModel)
        {
            _categoriaNegocio.Inserir(categoriaModel);
        }

        [HttpPut()]
        public void Put([FromBody] CategoriaModel categoriaModel)
        {
            _categoriaNegocio.Alterar(categoriaModel);
        }
        [HttpDelete()]
        public void Delete([FromQuery] int id)
        {
            _categoriaNegocio.Excluir(id);
        }
    }
}