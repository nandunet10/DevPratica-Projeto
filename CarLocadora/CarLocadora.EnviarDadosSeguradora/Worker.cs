using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Modelos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace CarLocadora.EnviarDadosSeguradora
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
                _logger.LogInformation("Serviço EnviarDadosSeguradora em execução: {0}", DateTimeOffset.Now);

                var canal = _factory.GetChannel();

                BasicGetResult retorno = canal.BasicGet("seguro", false);
                if (retorno != null)
                {
                    var dados = JsonConvert.DeserializeObject<LocacoesModel>(Encoding.UTF8.GetString(retorno.Body.ToArray()));
                    if (dados != null)
                    {
                        try
                        {
                            var seguro = new Seguro()
                            {
                                CPF = dados.Cliente.CPF,
                                CNH = dados.Cliente.CNH,
                                Nome = dados.Cliente.Nome,
                                DataNascimento = dados.Cliente.DataNascimento,
                                Telefone = dados.Cliente.Telefone,
                                Placa = dados.Veiculo.Placa,
                                Marca = dados.Veiculo.Marca,
                                Modelo = dados.Veiculo.Modelo,
                                Combustivel = dados.Veiculo.Combustivel,
                                DataHoraRetiradaPrevista = dados.DataHoraRetiradaPrevista,
                                DataHoraDevolucaoPrevista = dados.DataHoraDevolucaoPrevista,
                            };

                            var numeroProtocolo = await EnviarDadosSeguradora(seguro);
                            if (numeroProtocolo != 0)
                            {
                                //dados.NumeroProtocolo = numeroProtocolo;
                                var seguroProtocolo = new SeguroProtocolo()
                                {
                                    Id = dados.Id,
                                    NumeroProtocolo = numeroProtocolo
                                };

                                PublicarMensagem(seguroProtocolo, "", "seguro-protocolo");

                                _logger.LogInformation(seguroProtocolo.ToString());

                                canal.BasicAck(retorno.DeliveryTag, true);
                            }
                            else
                            {
                                _logger.LogWarning($"Não retorno protocolo.");
                                canal.BasicNack(retorno.DeliveryTag, false, true);
                            }

                        }
                        catch
                        {
                            _logger.LogError("Não foi possível realizar a operação");
                            canal.BasicNack(retorno.DeliveryTag, false, true);
                        }

                    }
                    else
                    {
                        _logger.LogWarning($"Não existe mensagem na fila.");
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

        private async Task<Int32> EnviarDadosSeguradora(Seguro model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiTokenSeguradora.Obter());
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Seguro", model);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Dados enviado para seguradora!");
                return JsonConvert.DeserializeObject<Int32>(await response.Content.ReadAsStringAsync());
            }
            else
                throw new Exception(response.ReasonPhrase);

        }
    }
}