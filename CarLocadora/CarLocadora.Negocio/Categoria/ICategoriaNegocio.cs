using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Categoria
{
    public interface ICategoriaNegocio
    {
        Task<List<CategoriaModel>> ObterLista();
        Task Inserir(CategoriaModel model);
        Task Alterar(CategoriaModel model);
        Task Excluir(int id);
        Task<CategoriaModel> Obter(int id);
    }
}
