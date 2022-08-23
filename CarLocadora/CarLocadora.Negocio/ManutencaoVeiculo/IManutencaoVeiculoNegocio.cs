
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.ManutencaoVeiculo
{
    public interface IManutencaoVeiculoNegocio
    {
        List<ManutencaoVeiculoModel> ObterLista();
        void Alterar(ManutencaoVeiculoModel model);
        void Inserir(ManutencaoVeiculoModel model);
        void Excluir(int id);
        ManutencaoVeiculoModel Obter(int id);
    }
}
