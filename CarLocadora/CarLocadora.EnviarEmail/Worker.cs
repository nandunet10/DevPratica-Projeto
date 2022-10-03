using AutoMapper;
using CarLocadora.EnviarEmail.Modelo;
using CarLocadora.Modelo.Modelos;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using static CarLocadora.EnviarEmail.Modelo.ClienteModelRetorno;

namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClient, IMapper mapper)
        {
            _logger = logger;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var emailCliente = await ObterClientesEnviarEmail();

                ClienteModel cliente = _mapper.Map<ClienteModel>(emailCliente);
                await EmailEnviado(cliente);


                await Task.Delay(1000, stoppingToken);
            }
        }
        private async Task<List<ClienteModelRetorno>> ObterClientesEnviarEmail()
        {
            HttpResponseMessage retorno = await _httpClient.GetAsync($"https://localhost:44339/api/Cliente/ObterListaEnviarEmail");
            if (retorno.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<ClienteModelRetorno>>(await retorno.Content.ReadAsStringAsync());
                return result;
            }
            else
            {
                throw new Exception(retorno.ReasonPhrase);
            }
        }

        private async Task EmailEnviado(ClienteModel cliente)
        {
            cliente.EmailEnviado = true;
            HttpResponseMessage retorno = await _httpClient.PostAsJsonAsync($"https://localhost:44339/api/Cliente/", cliente);

            if (retorno.IsSuccessStatusCode)
                _logger.LogInformation($"E-mail enviado com sucesso!");
            else
                throw new Exception(retorno.ReasonPhrase);

        }
    }
}