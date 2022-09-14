using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Cliente
{
    public interface IClienteNegocio
    {
        Task<List<ClienteModel>> ObterLista();
        Task Alterar(ClienteModel model);
        Task Inserir(ClienteModel model);
        Task<ClienteModel> ObterAsync(string cpf);
    }
}
