using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.FormasDePagamento
{
    public interface IFormasDePagamentoNegocio
    {
        Task<List<FormasDePagamentoModel>> ObterLista();
        Task Inserir(FormasDePagamentoModel model);
        Task Alterar(FormasDePagamentoModel model);
        Task<FormasDePagamentoModel> Obter(int id);
    }
}
