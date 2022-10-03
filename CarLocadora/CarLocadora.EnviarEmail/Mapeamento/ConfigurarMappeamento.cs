using AutoMapper;
using CarLocadora.EnviarEmail.Modelo;
using CarLocadora.Modelo.Modelos;

namespace CotacaoMoeda.Servico.Mapeamento
{
    public class ConfigurarMappeamento : Profile
    {
        public ConfigurarMappeamento()
        {
            CreateMap<ClienteModelRetorno, ClienteModel>();
                //.ForMember(dest => dest.Nome, opts => opts.MapFrom(src => src.name))
                //.ForMember(dest => dest.ValorCompra, opts => opts.MapFrom(src => src.USDBRL.bid))
                //.ForMember(dest => dest.ValorVenda, opts => opts.MapFrom(src => src.USDBRL.ask));
        }
    }
}
