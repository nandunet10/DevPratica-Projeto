using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Veiculo
{
    public interface IVeiculoNegocio
    {
        List<VeiculoModel> ObterLista();
        void Inserir(VeiculoModel model);
        void Alterar(VeiculoModel model);
    }
}
