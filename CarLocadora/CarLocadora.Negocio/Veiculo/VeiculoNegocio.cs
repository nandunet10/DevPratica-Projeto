
using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Veiculo
{
    public class VeiculoNegocio : IVeiculoNegocio
    {
        private readonly Context _context;

        public VeiculoNegocio(Context context)
        {
            _context = context;
        }

        public void Alterar(VeiculoModel model)
        {
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(VeiculoModel model)
        {
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }

        public List<VeiculoModel> ObterLista() => _context.Veiculos.ToList();

    }
}
