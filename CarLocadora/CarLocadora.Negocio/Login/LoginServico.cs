
using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio
{
    public class LoginServico
    {
        public async Task<LoginRespostaModel> Login(LoginRequisicaoModel loginRequisicaoModel)
        {
            LoginRespostaModel loginRespostaModel = new()
            {
                Autenticado = false,
                Usuario = loginRequisicaoModel.Usuario,
                Token = "",
                DataExpiracao = null
            };

            if (loginRequisicaoModel.Usuario == "UsuarioDevPratica" && loginRequisicaoModel.Senha == "SenhaDevPratica")
            {
                loginRespostaModel = new GeradorToken().GerarToken(loginRespostaModel);
            }

            return loginRespostaModel;
        }
    }
}
