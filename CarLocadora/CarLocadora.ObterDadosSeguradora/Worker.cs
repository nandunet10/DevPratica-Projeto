using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;
using CarLocadora.Infra.RabbitMQ;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http.Headers;
using System.Text;

namespace CarLocadora.ObterDadosSeguradora
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _factory;
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiTokenSeguradora;
        public Worker(ILogger<Worker> logger, RabbitMQFactory factory, IOptions<DadosBase> dadosBase, IHttpClientFactory httpClient, IApiToken apiTokenSeguradora)
        {
            _logger = logger;
            _factory = factory;
            _dadosBase = dadosBase;
            _apiTokenSeguradora = apiTokenSeguradora;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Serviço ObterDadosSeguradora em execução: {0}", DateTimeOffset.Now);

                var canal = _factory.GetChannel();

                BasicGetResult retorno = canal.BasicGet("seguro-protocolo", false);
                if (retorno != null)
                {
                    try
                    {
                        var dados = JsonConvert.DeserializeObject<SeguroProtocolo>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                        if (dados != null)
                        {
                            var statusSeguradora = await ObterStatuSeguradora(dados.NumeroProtocolo.Value);

                            if (statusSeguradora != null && statusSeguradora.Status.ToLower() != "processando" && statusSeguradora.Status.ToLower() != "erro")
                            {
                                var locacao = new LocacoesSeguro()
                                {
                                    Id = dados.Id,
                                    SeguroApolice = statusSeguradora.Apolice,
                                    SeguroAprovado = statusSeguradora.Status.ToLower() == "aprovado" ? true : false,
                                    SeguroObservacao = statusSeguradora.Observacao
                                };

                                PublicarMensagem(locacao, "", "seguro-dados-retorno");

                                _logger.LogInformation(locacao.ToString());

                                canal.BasicAck(retorno.DeliveryTag, true);
                            }
                            else
                            {
                                _logger.LogInformation($"Status {statusSeguradora.Status}, não é possível seguir com a requisição ");
                                canal.BasicNack(retorno.DeliveryTag, false, true);
                            }

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

        private void PublicarMensagem(object obj, string exchange, string fila)
        {
            var canal = _factory.GetChannel();
            IBasicProperties iBasicProperties = canal.CreateBasicProperties();

            var corpoMensagem = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

            canal.BasicPublish(exchange: exchange, routingKey: fila, basicProperties: iBasicProperties, body: corpoMensagem);
        }

        private async Task<SeguroStatus> ObterStatuSeguradora(Int32 numeroProtocolo)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiTokenSeguradora.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Seguro?protocolo={numeroProtocolo}");

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Dados enviado para seguradora!");
                return JsonConvert.DeserializeObject<SeguroStatus>(await response.Content.ReadAsStringAsync());
            }
            else
                throw new Exception(response.ReasonPhrase);

        }
    }
}