using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Usuario
{
    public interface IUsuarioNegocio
    {
        Task<List<UsuarioModel>> ObterLista();
        Task Inserir(UsuarioModel model);
        Task Alterar(UsuarioModel model);
        Task<UsuarioModel> Obter(string cpf);
    }
}
