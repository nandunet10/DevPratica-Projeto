using CarLocadora.Comum.Modelos;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Locacoes
{
    public interface ILocacoesNegocio
    {
        Task<List<LocacoesModel>> ObterLista();
        Task Inserir(LocacoesModel model);
        Task Alterar(LocacoesModel model);
        Task AlterarDadosSeguro(LocacoesSeguro model);
        Task<LocacoesModel> Obter(int id);
    }
}
