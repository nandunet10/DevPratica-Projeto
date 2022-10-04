using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Front.Controllers
{
    public class ClienteController : Controller
    {
        private string mensagem = String.Empty;

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginRespostaModel> _loginRespostaModel;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;

        public ClienteController(IOptions<DadosBase> dadosBase, IOptions<LoginRespostaModel> loginRespostaModel, IApiToken apiToken, IHttpClientFactory httpClient)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
            _apiToken = apiToken;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(string? mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<List<ClienteModel>>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception("Não foi possível carregar as informações!");

        }

        // GET: ClienteController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClienteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());

                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente", model);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro criado!", sucesso = true });
                    else
                        throw new Exception("Não foi possível carregar as informações!");
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

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(string CPF)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente/ObterDados?CPF={CPF}").Result;

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<ClienteModel>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception("Não foi possível carregar as informações!");

        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ClienteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Cliente", model);

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index), new { mensagem = "Registro editado!", sucesso = true });
                    else
                        throw new Exception("Não foi possível carregar as informações!");
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

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(string CPF)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
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
