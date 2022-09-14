using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.FormasDePagamento
{
    public class FormasDePagamentoNegocio : IFormasDePagamentoNegocio
    {
        private readonly Context _context;

        public FormasDePagamentoNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(FormasDePagamentoModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(FormasDePagamentoModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<FormasDePagamentoModel> Obter(int id) => await _context.FormasDePagamento.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<List<FormasDePagamentoModel>> ObterLista() => await _context.FormasDePagamento.ToListAsync();

    }
}
