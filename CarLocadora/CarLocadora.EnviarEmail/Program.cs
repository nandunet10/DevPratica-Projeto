using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.EnviarEmail;
using CarLocadora.Modelo.Modelos;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();
        services.AddSingleton<IApiToken, ApiToken>();
        services.AddSingleton<LoginRespostaModel>();

        services.Configure<DadosBase>(hostContext.Configuration.GetSection("DadosBase"));

        services.AddHostedService<Worker>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    })
    .Build();

await host.RunAsync();
