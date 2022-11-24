using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Modelos;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Carlocadora.GerarArquivo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _factory;

        public Worker(ILogger<Worker> logger, RabbitMQFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var canal = _factory.GetChannel();

                BasicGetResult retorno = canal.BasicGet("cadastrarVeiculo", false);
                if (retorno == null)
                {
                    break;
                }
                else
                {
                    var dados = JsonConvert.DeserializeObject<VeiculoModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    var texto = @$"c:\Teste\{dados.Placa.Replace("-", "")}.txt";

                    using (var sw = new StreamWriter(texto))
                    {
                        await sw.WriteLineAsync($"Placa: {dados.Placa}");
                        await sw.WriteLineAsync($"Chassi: {dados.Chassi}");
                        await sw.WriteLineAsync($"Marca: {dados.Marca}");
                        await sw.WriteLineAsync($"Modelo: {dados.Modelo}");
                        await sw.WriteLineAsync($"Combustível: {dados.Combustivel}");
                        await sw.WriteLineAsync($"Cor: {dados.Cor}");
                        await sw.WriteLineAsync($"Opcional: {dados.Opcionais}");
                        await sw.WriteLineAsync($"Ativo: {(dados.Ativo == true ? "sim" : "não")}");
                        await sw.WriteLineAsync($"Data inclusão: {dados.DataInclusao}");
                        await sw.WriteLineAsync($"Data alteração: {dados.DataAlteracao}");
                        await sw.WriteLineAsync($"CategoriaId: {dados.CategoriaId}");
                        //await sw.WriteLineAsync($"Categoria: {(dados.Categoria == null ? "" : dados.Categoria.ToString())}");
                    }
                    canal.BasicAck(retorno.DeliveryTag, true);

                }
                _logger.LogInformation("Serviço em execução: {0}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}