using CarLocadora.Front.Servico;
using CarLocadora.Modelo.Modelos;
using CarLocadora.Servico;

namespace CarLocadora.API.Extensoes;

public static class ServicoExtensoes
{
    public static void ConfigurarServicos(this IServiceCollection services)
    {
        //Adicionar Scoped
        services.AddSingleton<IApiToken, ApiToken>();
        services.AddSingleton<LoginRespostaModel>();

        services.AddHttpClient();

    }

    public static void ConfigurarAPI(this IServiceCollection services)
    { 

    }
}
