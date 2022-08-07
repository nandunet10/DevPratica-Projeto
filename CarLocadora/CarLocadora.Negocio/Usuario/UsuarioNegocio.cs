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
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(UsuarioModel model)
        {
            model.DataInclusao = DateTime.Now;
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public UsuarioModel Obter(string cpf)
        {
            return _context.Usuarios.SingleOrDefault(x => x.CPF.Equals(cpf));
        }

        public List<UsuarioModel> ObterLista() => _context.Usuarios.ToList();
  
    }
}
