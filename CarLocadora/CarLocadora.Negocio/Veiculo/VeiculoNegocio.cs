
using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Veiculo
{
    public class VeiculoNegocio : IVeiculoNegocio
    {
        private readonly Context _context;

        public VeiculoNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(VeiculoModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(VeiculoModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<VeiculoModel> Obter(string placa) => await _context.Veiculos.SingleOrDefaultAsync(x => x.Placa.Equals(placa));    

        public async Task<List<VeiculoModel>> ObterLista() => await _context.Veiculos.ToListAsync();

    }
}
