using CarLocadora.Comum.Modelos;
using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using CarLocadora.Negocio.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Negocio.Locacoes
{
    public class LocacoesNegocio : ILocacoesNegocio
    {
        private readonly Context _context;
        private readonly IMensageriaNegocio _rabbitMQNegocio;
        public LocacoesNegocio(Context context, IMensageriaNegocio rabbitMQNegocio)
        {
            _context = context;
            _rabbitMQNegocio = rabbitMQNegocio;
        }

        public async Task Alterar(LocacoesModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Locacoes.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarDadosSeguro(LocacoesSeguro model)
        {
            var locacao = _context.Locacoes.SingleOrDefault(x => x.Id.Equals(model.Id));
            try
            {
                locacao.SeguroApolice = model.SeguroApolice;
                locacao.SeguroAprovado = model.SeguroAprovado;
                locacao.SeguroObservacao = model.SeguroObservacao;

                _context.Locacoes.Update(locacao);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }

        }

        public async Task Inserir(LocacoesModel locacoes)
        {
            locacoes.DataInclusao = DateTime.Now;
            await _context.Locacoes.AddAsync(locacoes);
            await _context.SaveChangesAsync();

            locacoes.Cliente = _context.Clientes.SingleOrDefault(x => x.CPF == locacoes.ClienteCPF);
            locacoes.Veiculo = _context.Veiculos.SingleOrDefault(x => x.Placa == locacoes.VeiculoPlaca);

            //var seguro = new SeguroProtocoloModel()
            //{
            //    Id = model.Id,
            //    CPF = cliente.CPF,
            //    CNH = cliente.CNH,
            //    Nome = cliente.Nome,
            //    DataNascimento = cliente.DataNascimento,
            //    Telefone = cliente.Telefone,
            //    Placa = veiculo.Placa,
            //    Marca = veiculo.Marca,
            //    Modelo = veiculo.Modelo,
            //    Combustivel = veiculo.Combustivel,
            //    DataHoraRetiradaPrevista = model.DataHoraRetiradaPrevista,
            //    DataHoraDevolucaoPrevista = model.DataHoraDevolucaoPrevista
            //};

            _rabbitMQNegocio.PublicarMensagem(locacoes, "", "seguro");
        }

        public async Task<LocacoesModel> Obter(int id)
        {
            return await _context.Locacoes.SingleOrDefaultAsync(x => x.Id.Equals(id));

        }

        public async Task<List<LocacoesModel>> ObterLista()
        {
            return await _context.Locacoes.ToListAsync();
        }
    }
}
