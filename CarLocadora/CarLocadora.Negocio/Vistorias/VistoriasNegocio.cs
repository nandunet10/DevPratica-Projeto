using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Vistorias
{
    public class VistoriasNegocio : IVistoriasNegocio
    {
        private readonly Context _context;
        public VistoriasNegocio(Context context)
        {
            _context = context;
        }

        public void Alterar(VistoriasModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Vistorias.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(VistoriasModel model)
        {
            model.DataInclusao = DateTime.Now;
            _context.Vistorias.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public VistoriasModel Obter(int id) => _context.Vistorias.SingleOrDefault(x => x.Id.Equals(id));

        public List<VistoriasModel> ObterLista()
        {
            return _context.Vistorias.ToList();
        }
    }
}
