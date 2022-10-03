using CarLocadora.EnviarEmail;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddHostedService<Worker>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    })
    .Build();

await host.RunAsync();
