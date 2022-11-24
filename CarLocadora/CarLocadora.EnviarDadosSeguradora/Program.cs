using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;
using CarLocadora.EnviarDadosSeguradora;
using CarLocadora.Infra.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        #region Acesso a api
        services.AddHttpClient();
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
