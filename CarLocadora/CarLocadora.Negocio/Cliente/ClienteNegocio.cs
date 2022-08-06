using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Cliente
{
    public class ClienteNegocio : IClienteNegocio
    {
        private readonly Context _context;
        public ClienteNegocio(Context context)
        {
            _context = context;
        }
        public void Alterar(ClienteModel model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(ClienteModel model)
        {
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }
        public List<ClienteModel> ObterLista() => _context.Clientes.ToList();
    }
}
