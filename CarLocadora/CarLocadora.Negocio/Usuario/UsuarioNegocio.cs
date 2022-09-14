using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Usuario
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private readonly Context _context;

        public UsuarioNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(UsuarioModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(UsuarioModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<UsuarioModel> Obter(string cpf) => await _context.Usuarios.SingleOrDefaultAsync(x => x.CPF.Equals(cpf));        

        public async Task<List<UsuarioModel>> ObterLista() => await _context.Usuarios.ToListAsync();

    }
}
