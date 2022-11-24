using CarLocadora.AtualizarDadosLocacaoSeguradora;
using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;
using CarLocadora.Infra.RabbitMQ;
using CarLocadora.Modelo.Modelos;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        #region Acesso a api
        //services.AddHttpClient();
        services.AddHttpClient("").ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
        });
        services.AddSingleton<IApiToken, ApiToken>();
        services.AddSingleton<LoginResposta>();

        services.Configure<DadosBase>(hostContext.Configuration.GetSection("DadosBase"));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        #endregion

        #region RabbitMQ
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddSingleton<RabbitMQFactory>();

        #endregion
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();