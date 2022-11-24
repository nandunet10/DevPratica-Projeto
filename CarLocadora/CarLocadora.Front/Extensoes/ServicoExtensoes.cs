using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;

namespace CarLocadora.API.Extensoes;

public static class ServicoExtensoes
{
    public static void ConfigurarServicos(this IServiceCollection services)
    {
        //Adicionar Scoped
        services.AddSingleton<IApiToken, ApiToken>();
        services.AddSingleton<LoginResposta>();

        services.AddHttpClient();

    }

    public static void ConfigurarAPI(this IServiceCollection services)
    {

    }
}
