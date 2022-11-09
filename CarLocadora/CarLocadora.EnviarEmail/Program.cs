using CarLocadora.Comum.Modelo;
using CarLocadora.EnviarEmail;
using CarLocadora.Infra.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        #region Direto do banco de dados
        //services.AddHttpClient();
        //services.AddSingleton<IApiToken, ApiToken>();
        //services.AddSingleton<LoginRespostaModel>();

        //services.Configure<DadosBase>(hostContext.Configuration.GetSection("DadosBase"));

        //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        #endregion

        #region RabbitMQ
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddSingleton<RabbitMQFactory>();

        #endregion

        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
