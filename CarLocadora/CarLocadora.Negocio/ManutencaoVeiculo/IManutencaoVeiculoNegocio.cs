
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.ManutencaoVeiculo
{
    public interface IManutencaoVeiculoNegocio
    {
        Task<List<ManutencaoVeiculoModel>> ObterLista();
        Task Alterar(ManutencaoVeiculoModel model);
        Task Inserir(ManutencaoVeiculoModel model);
        Task Excluir(int id);
        Task<ManutencaoVeiculoModel> Obter(int id);
    }
}
