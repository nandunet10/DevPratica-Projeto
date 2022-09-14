using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Vistorias
{
    public interface IVistoriasNegocio
    {
        Task<List<VistoriasModel>> ObterLista();
        Task Inserir(VistoriasModel model);
        Task Alterar(VistoriasModel model);
        Task<VistoriasModel> Obter(int id);
    }
}
