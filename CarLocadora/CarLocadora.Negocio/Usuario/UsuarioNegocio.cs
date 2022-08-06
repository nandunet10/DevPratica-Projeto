using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Usuario
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private readonly Context _context;

        public UsuarioNegocio(Context context)
        {
            _context = context;
        }

        public void Alterar(UsuarioModel model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(UsuarioModel model)
        {
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public List<UsuarioModel> ObterLista() => _context.Usuarios.ToList();
  
    }
}
