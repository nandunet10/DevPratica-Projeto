using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Cliente
{
    public class ClienteNegocio : IClienteNegocio
    {
        private readonly Context _context;
        public ClienteNegocio(Context context)
        {
            _context = context;
        }
        public async Task Alterar(ClienteModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(ClienteModel model)
        {
            model.DataInclusao = DateTime.Now;
            _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<ClienteModel> ObterAsync(string cpf) => await _context.Clientes.SingleOrDefaultAsync(x => x.CPF.Equals(cpf));

        public async Task<List<ClienteModel>> ObterLista() => await _context.Clientes.ToListAsync();

        public async Task<List<ClienteModel>> ObterListaEnviarEmail()
        {
            return await _context.Clientes.Where(x => x.Email != null && x.EmailEnviado.Equals(false)).ToListAsync();
        }
    }
}
