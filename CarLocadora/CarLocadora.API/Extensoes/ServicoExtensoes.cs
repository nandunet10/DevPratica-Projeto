using AspNetCoreRateLimit;
using CarLocadora.Negocio.Categoria;
using CarLocadora.Negocio.Cliente;
using CarLocadora.Negocio.FormasDePagamento;
using CarLocadora.Negocio.Locacoes;
using CarLocadora.Negocio.ManutencaoVeiculo;
using CarLocadora.Negocio.Usuario;
using CarLocadora.Negocio.Veiculo;
using CarLocadora.Negocio.Vistorias;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography;


namespace CarLocadora.API.Extensoes;

public static class ServicoExtensoes
{

    public static void ConfigurarSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API - Dev:Prática",
                Version = "v1",
                Description = "Api de locadora de veículos"
            });

            c.EnableAnnotations();

            var securityscheme = new OpenApiSecurityScheme
            {
                Name = "autenticação jwt",
                Description = "informe o token jwt beare **_somente_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityscheme.Reference.Id, securityscheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { securityscheme, Array.Empty<string>() }
                    });
        });
    public static void ConfigurarJWT(this IServiceCollection services)
    {
        Environment.SetEnvironmentVariable("JWT_SECRETO",
         Convert.ToBase64String(new HMACSHA256().Key), EnvironmentVariableTarget.Process);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = "EmitenteDoJWT",
                ValidAudience = "DestinatarioDoJWT",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Convert.FromBase64String(Environment.GetEnvironmentVariable("JWT_SECRETO"))),

            };

        });

    }

    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "post:/api/Login",
                Limit = 2,
                Period = "10s",
            },
            //new RateLimitRule
            //{
            //    Endpoint = "*",
            //    Period = "10s",
            //    Limit = 2
            //}
        };

        services.Configure<IpRateLimitOptions>(opt =>
        {
            opt.EnableEndpointRateLimiting = true;
            opt.StackBlockedRequests = false;
            opt.GeneralRules = rateLimitRules;
        });

        services.AddInMemoryRateLimiting();

        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    }

    public static void ConfigurarServicos(this IServiceCollection services)
    {
        //string connection = Configuration.("DefaultConnection");
        //string connection = "Data Source=localhost,1433;Initial Catalog=DBCarLocadora;User ID=sa;Password=senha@1234;";
        //services.AddDbContext<Context>(options => options.UseSqlServer(connection));

        //Adicionar Scoped
        services.AddScoped<IClienteNegocio, ClienteNegocio>();
        services.AddScoped<ICategoriaNegocio, CategoriaNegocio>();
        services.AddScoped<IFormasDePagamentoNegocio, FormasDePagamentoNegocio>();
        services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
        services.AddScoped<IVeiculoNegocio, VeiculoNegocio>();
        services.AddScoped<IManutencaoVeiculoNegocio, ManutencaoVeiculoNegocio>();
        services.AddScoped<ILocacoesNegocio, LocacoesNegocio>();
        services.AddScoped<IVistoriasNegocio, VistoriasNegocio>();

    }
}
