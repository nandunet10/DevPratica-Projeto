using CarLocadora.Front.Models;
using CarLocadora.Modelo.Modelos;
using CarLocadora.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Front.Controllers
{
    public class UsuarioController : Controller
    {
        private string? mensagem = string.Empty;

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginRespostaModel> _loginRespostaModel;
        public UsuarioController(IOptions<DadosBase> dadosBase, IOptions<LoginRespostaModel> loginRespostaModel)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
        }

        // GET: UsuarioController
        public ActionResult Index(string? mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;

            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = client.GetAsync($"{_dadosBase.Value.API_URL_BASE}Usuario").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<List<UsuarioModel>>(conteudo));
            }
            else
            {
                throw new Exception("Deu Zica");
            }
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    HttpClient client = new();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = client.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Usuario", model).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro criado!", sucesso = true });
                    }
                    else
                    {
                        throw new Exception("Deu Zica");
                    }

                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando o seu preenchimento!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["erro"] = "Algum erro aconteceu " + ex.Message;

                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(string CPF)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new ApiToken(_dadosBase, _loginRespostaModel).Obter());

            HttpResponseMessage response = client.GetAsync($"{_dadosBase.Value.API_URL_BASE}Usuario/ObterDados?CPF={CPF}").Result;

            if (response.IsSuccessStatusCode)
            {
                string conteudo = response.Content.ReadAsStringAsync().Result;
                return View(JsonConvert.DeserializeObject<UsuarioModel>(conteudo));
            }
            else
            {
                throw new Exception("Deu Zica");
            }
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] UsuarioModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpClient client = new();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new ApiToken(_dadosBase, _loginRespostaModel).Obter());

                    HttpResponseMessage response = client.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Usuario", model).Result;

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado!", sucesso = true });
                    else
                        throw new Exception("Deu Zica");

                }
                else
                {
                    TempData["erro"] = "Algum campo deve estar faltando o seu preenchimento!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["erro"] = "Algum erro aconteceu " + ex.Message;

                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
