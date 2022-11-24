using Carlocadora.GerarArquivo;
using CarLocadora.Comum.Modelos;
using CarLocadora.Infra.RabbitMQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        #region RabbitMQ
        services.Configure<DadosBaseRabbitMQ>(hostContext.Configuration.GetSection("DadosBaseRabbitMQ"));
        services.AddSingleton<RabbitMQFactory>();
        #endregion

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
