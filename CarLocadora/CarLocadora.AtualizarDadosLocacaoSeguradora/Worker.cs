using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;
using CarLocadora.Infra.RabbitMQ;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CarLocadora.AtualizarDadosLocacaoSeguradora
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _factory;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;
        public Worker(ILogger<Worker> logger, RabbitMQFactory factory, IOptions<DadosBase> dadosBase, IHttpClientFactory httpClient, IApiToken apiToken)
        {
            _logger = logger;
            _factory = factory;
            _dadosBase = dadosBase;
            _apiToken = apiToken;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Serviço AtualizarDadosLocaçãoSeguradora em execução: {0}", DateTimeOffset.Now);

                var canal = _factory.GetChannel();

                BasicGetResult retorno = canal.BasicGet("seguro-dados-retorno", false);
                if (retorno != null)
                {
                    try
                    {
                        var dados = JsonConvert.DeserializeObject<LocacoesSeguro>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                        if (dados != null)
                        {
                            //metodo de persistir no banco
                            await InserirDadoStatus(dados);

                            _logger.LogInformation(dados.ToString());

                            canal.BasicAck(retorno.DeliveryTag, true);
                        }
                        else
                        {
                            _logger.LogInformation($"Não existe mensagem na fila.");
                            canal.BasicNack(retorno.DeliveryTag, false, true);

                        }
                    }
                    catch
                    {
                        _logger.LogInformation("Não foi possível realizar a operação");
                        canal.BasicNack(retorno.DeliveryTag, false, true);
                    }
                }
                await Task.Delay(35000, stoppingToken);
            }
        }

        private async Task InserirDadoStatus(LocacoesSeguro model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage retorno = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Locacao/AtualizarDadosSeguro", model);

            if (retorno.IsSuccessStatusCode)
                _logger.LogInformation($"Dados atualizados com suscesso!");
            else
                throw new Exception(retorno.ReasonPhrase);

        }
    }
}