using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Categoria
{
    public class CategoriaNegocio : ICategoriaNegocio
    {
        private readonly Context _context;

        public CategoriaNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(CategoriaModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            CategoriaModel model = await _context.Categorias.SingleOrDefaultAsync(predicate: x => x.Id.Equals(id));
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(CategoriaModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoriaModel> Obter(int id) => await _context.Categorias.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<List<CategoriaModel>> ObterLista() => await _context.Categorias.ToListAsync();

    }
}
