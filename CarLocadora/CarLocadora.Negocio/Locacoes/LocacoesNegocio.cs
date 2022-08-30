using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Locacoes
{
    public class LocacoesNegocio : ILocacoesNegocio
    {
        private readonly Context _context;
        public LocacoesNegocio(Context context)
        {
            _context = context;
        }

        public void Alterar(LocacoesModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Locacoes.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(LocacoesModel model)
        {
            model.DataInclusao = DateTime.Now;
            _context.Locacoes.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public LocacoesModel Obter(int id)
        {
            return _context.Locacoes.SingleOrDefault(x => x.Id.Equals(id));

        }

        public List<LocacoesModel> ObterLista()
        {
            return _context.Locacoes.ToList();
        }
    }
}
