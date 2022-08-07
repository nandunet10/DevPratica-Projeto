using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Categoria
{
    public interface ICategoriaNegocio
    {
        List<CategoriaModel> ObterLista();
        void Inserir(CategoriaModel model);
        void Alterar(CategoriaModel model);
        void Excluir(int id);
        CategoriaModel Obter(int id);
    }
}
