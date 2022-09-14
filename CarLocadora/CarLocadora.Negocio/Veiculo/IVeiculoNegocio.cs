using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Veiculo
{
    public interface IVeiculoNegocio
    {
        Task<List<VeiculoModel>> ObterLista();
        Task Inserir(VeiculoModel model);
        Task Alterar(VeiculoModel model);
        Task<VeiculoModel> Obter(string placa);
    }
}
