﻿using CarLocadora.Comum.Modelos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CarLocadora.Comum.Servico
{
    public class ApiToken : IApiToken
    {
        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginResposta> _loginRespostaModel;

        public ApiToken(IOptions<DadosBase> dadosBase, IOptions<LoginResposta> loginRespostaModel)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
        }

        private async Task ObterToken()
        {
            HttpClient cliente = new();
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //LoginRequisicaoModel loginRequisicaoModel = new()
            //{
            //    Usuario = "UsuarioDevPratica",
            //    Senha = "SenhaDevPratica"
            //};

            HttpResponseMessage response = await cliente.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Login", new LoginRequisicao() { Usuario = _dadosBase.Value.USUARIO, Senha = _dadosBase.Value.SENHA });

            if (response.IsSuccessStatusCode)
            {
                LoginResposta loginRespostaModel = JsonConvert.DeserializeObject<LoginResposta>(await response.Content.ReadAsStringAsync());

                if (loginRespostaModel.Autenticado == true)
                {
                    _loginRespostaModel.Value.Autenticado = loginRespostaModel.Autenticado;
                    _loginRespostaModel.Value.Usuario = loginRespostaModel.Usuario;
                    _loginRespostaModel.Value.DataExpiracao = loginRespostaModel.DataExpiracao;
                    _loginRespostaModel.Value.Token = loginRespostaModel.Token;
                }
            }
            else
            {
                throw new Exception("Falha na autenticação, por favor tente novamente !");
            }
        }

        public async Task<string> Obter()
        {
            if (_loginRespostaModel.Value.Autenticado == false)
            {
                await ObterToken();
            }
            else
            {
                if (DateTime.Now >= _loginRespostaModel.Value.DataExpiracao)
                {
                    await ObterToken();
                }
            }
            return _loginRespostaModel.Value.Token;
        }
    }
}