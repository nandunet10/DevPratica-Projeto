using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Locacoes
{
    public class LocacoesNegocio : ILocacoesNegocio
    {
        private readonly Context _context;
        public LocacoesNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(LocacoesModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Locacoes.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(LocacoesModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.Locacoes.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<LocacoesModel> Obter(int id)
        {
            return await _context.Locacoes.SingleOrDefaultAsync(x => x.Id.Equals(id));

        }

        public async Task<List<LocacoesModel>> ObterLista()
        {
            return await _context.Locacoes.ToListAsync();
        }
    }
}
