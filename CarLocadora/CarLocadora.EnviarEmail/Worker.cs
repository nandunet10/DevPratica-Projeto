using CarLocadora.EnviarEmail.Modelo;
using CarLocadora.Infra.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CarLocadora.EnviarEmail
{
    public class Worker : BackgroundService
    {
        //private readonly IOptions<DadosBase> _dadosBase;
        //private readonly HttpClient _httpClient;
        //private readonly IApiToken _apiToken;

        //public Worker(ILogger<Worker> logger, IHttpClientFactory httpClient, IApiToken apiToken, IOptions<DadosBase> dadosBase)
        //{
        //    _logger = logger;
        //    _httpClient = httpClient.CreateClient();
        //    _apiToken = apiToken;
        //    _dadosBase = dadosBase;

        //    _factory = new ConnectionFactory
        //    {
        //        HostName = "localhost",
        //        Port = 5672,
        //        UserName = "guest",
        //        Password = "guest"
        //    };
        //}

        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQFactory _factory;

        public Worker(ILogger<Worker> logger, RabbitMQFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        //        var emailCliente = await ObterClientesEnviarEmail();

        //        foreach (var cliente in emailCliente)
        //        {
        //            try
        //            {
        //                await EnviarEmail(cliente.Email, cliente.Nome);
        //                await AtualizarCliente(cliente.Cpf);

        //            }
        //            catch
        //            {
        //                continue;
        //            }
        //        }

        //        await Task.Delay(1000, stoppingToken);
        //    }
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Serviço em execução ás: {0}", DateTimeOffset.Now);

                var canal = _factory.GetChannel();

                BasicGetResult retorno = canal.BasicGet("cadastro", false);
                if (retorno == null)
                {
                    break;
                }
                else
                {
                    var dados = JsonConvert.DeserializeObject<ClienteModelRetorno>(Encoding.UTF8.GetString(retorno.Body.ToArray()));

                    EnviarEmail(dados.Email, dados.Nome);

                    canal.BasicAck(retorno.DeliveryTag, true);
                }


                await Task.Delay(1000, stoppingToken);
            }
        }
        #region Disparo de e-mail
        private static void EnviarEmail(string email, string nome)
        {
            MailMessage mensagem = new()
            {
                From = new MailAddress("turma1@devpratica.com.br"),
                Subject = "Bem-Vindo!!",
                IsBodyHtml = true,
                Body = EmailBoasVindas(nome)
            };
            mensagem.To.Add(email);

            var smtpCliente = new SmtpClient("smtp.kinghost.net")
            {
                Port = 587,
                Credentials = new NetworkCredential("turma1@devpratica.com.br", "Senha@senha10"),
                EnableSsl = false,

            };
            smtpCliente.Send(mensagem);
        }

        private static string EmailBoasVindas(string nome)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<p>Parabéns <b>{nome},</b></p>");
            sb.Append($"<p>Seja muito bem-vindo a <b>CAR-LOCADORA.</b></p>");
            sb.Append($"<p>Estamos muito felizes de você fazer parte da <b>CAR-LOCADORA</b>.</p>");
            sb.Append($"<br>");
            sb.Append($"<p>Grande abraço</p>");
            return sb.ToString();
        }
        #endregion

        #region Metodos de api
        //private async Task<List<ClienteModelRetorno>> ObterClientesEnviarEmail()
        //{
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
        //    HttpResponseMessage retorno = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterListaEnviarEmail");

        //    if (retorno.IsSuccessStatusCode)
        //    {
        //        var result = JsonConvert.DeserializeObject<List<ClienteModelRetorno>>(await retorno.Content.ReadAsStringAsync());
        //        return result;
        //    }
        //    else
        //    {
        //        throw new Exception(retorno.ReasonPhrase);
        //    }
        //}
        //private async Task AtualizarCliente(string cpf)
        //{
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
        //    HttpResponseMessage retorno = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/AlterarEnvioDeEmail", cpf);

        //    if (retorno.IsSuccessStatusCode)
        //        _logger.LogInformation($"E-mail enviado com sucesso!");
        //    else
        //        throw new Exception(retorno.ReasonPhrase);

        //}
        #endregion

        //private async Task<ClienteModelRetorno?> ObterMensagem(string nomeFila)
        //{
        //    var conectarRabbit = _factory.CreateConnection();
        //    var canal = conectarRabbit.CreateModel();

        //    BasicGetResult retorno = canal.BasicGet(nomeFila, false);
        //    if (retorno != null)
        //    {
        //        var cliente = JsonConvert.DeserializeObject<ClienteModelRetorno>(Encoding.UTF8.GetString(retorno.Body.ToArray()));

        //        await EnviarEmail(cliente.Email, cliente.Nome);

        //        canal.BasicAck(retorno.DeliveryTag, true);
        //    }
        //    return null;

        //}
    }
}