
using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Veiculo
{
    public class VeiculoNegocio : IVeiculoNegocio
    {
        private readonly Context _context;
        private readonly IMensageriaNegocio _rabbitMQNegocio;

        public VeiculoNegocio(Context context, IMensageriaNegocio rabbitMQNegocio)
        {
            _context = context;
            _rabbitMQNegocio = rabbitMQNegocio;
        }

        public async Task Alterar(VeiculoModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();

            _rabbitMQNegocio.PublicarMensagem(model, "", "cadastrarVeiculo");
        }

        public async Task Inserir(VeiculoModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();

            _rabbitMQNegocio.PublicarMensagem(model, "", "cadastrarVeiculo");

        }

        public async Task<VeiculoModel> Obter(string placa) => await _context.Veiculos.SingleOrDefaultAsync(x => x.Placa.Equals(placa));    

        public async Task<List<VeiculoModel>> ObterLista() => await _context.Veiculos.ToListAsync();

    }
}
