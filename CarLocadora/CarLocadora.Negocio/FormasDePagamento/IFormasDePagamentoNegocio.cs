using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.FormasDePagamento
{
    public interface IFormasDePagamentoNegocio
    {
        List<FormasDePagamentoModel> ObterLista();
        void Inserir(FormasDePagamentoModel model);
        void Alterar(FormasDePagamentoModel model);
        FormasDePagamentoModel Obter(int id);
    }
}
