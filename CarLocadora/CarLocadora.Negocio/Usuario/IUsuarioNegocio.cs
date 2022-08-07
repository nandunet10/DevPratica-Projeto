using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Usuario
{
    public interface IUsuarioNegocio
    {
        List<UsuarioModel> ObterLista();
        void Inserir(UsuarioModel model);
        void Alterar(UsuarioModel model);
        UsuarioModel Obter(string cpf);
    }
}
