using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.FormasDePagamento
{
    public class FormasDePagamentoNegocio : IFormasDePagamentoNegocio
    {
        private readonly Context _context;

        public FormasDePagamentoNegocio(Context context)
        {
            _context = context;
        }

        public void Alterar(FormasDePagamentoModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(FormasDePagamentoModel model)
        {
            model.DataInclusao = DateTime.Now;
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public FormasDePagamentoModel Obter(int id)
        {
            return _context.FormasDePagamento.SingleOrDefault(x => x.Id.Equals(id));
        }

        public List<FormasDePagamentoModel> ObterLista() => _context.FormasDePagamento.ToList();

    }
}
