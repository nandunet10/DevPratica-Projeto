using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.EnviarEmail.Modelo;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClient, IApiToken apiToken, IOptions<DadosBase> dadosBase)
        {
            _logger = logger;
            _httpClient = httpClient.CreateClient();
            _apiToken = apiToken;
            _dadosBase = dadosBase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var emailCliente = await ObterClientesEnviarEmail();

                foreach (var cliente in emailCliente)
                {
                    try
                    {
                        await EnviarEmail(cliente.Email, cliente.Nome);
                        await AtualizarCliente(cliente);

                    }
                    catch
                    {
                        continue;
                    }
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
        private async Task<List<ClienteModelRetorno>> ObterClientesEnviarEmail()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage retorno = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterListaEnviarEmail");

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

        private async Task EnviarEmail(string email, string nome)
        {
            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress("turma1@devpratica.com.br");
            mensagem.To.Add(email);
            mensagem.Subject = "Bem-Vindo!!";
            mensagem.IsBodyHtml = true;
            mensagem.Body = EmailBoasVindas(nome);

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(mensagem);
        }

        private string EmailBoasVindas(string nome)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Parabéns <b>{nome},</b></p>");
            sb.Append($"<p>Seja muito bem-vindo a <b>CAR-LOCADORA.</b></p>");
            sb.Append($"<p>Estamos muito felizes de você fazer parte da <b>CAR-LOCADORA</b>.</p>");
            sb.Append($"<br>");
            sb.Append($"<p>Grande abraço</p>");
            return sb.ToString();
        }

        private async Task AtualizarCliente(ClienteModelRetorno cliente)
        {
            cliente.EmailEnviado = true;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage retorno = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente", cliente);

            if (retorno.IsSuccessStatusCode)
                _logger.LogInformation($"E-mail enviado com sucesso!");
            else
                throw new Exception(retorno.ReasonPhrase);

        }
    }
}