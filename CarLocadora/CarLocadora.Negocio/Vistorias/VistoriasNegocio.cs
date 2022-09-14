using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Vistorias
{
    public class VistoriasNegocio : IVistoriasNegocio
    {
        private readonly Context _context;
        public VistoriasNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(VistoriasModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Vistorias.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(VistoriasModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.Vistorias.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<VistoriasModel> Obter(int id) => await _context.Vistorias.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<List<VistoriasModel>> ObterLista() => await _context.Vistorias.ToListAsync();
        
    }
}
