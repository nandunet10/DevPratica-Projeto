using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Front.Controllers
{
    public class FormasDePagamentoController : Controller
    {
        private string? mensagem = string.Empty;

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginRespostaModel> _loginRespostaModel;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;
        public FormasDePagamentoController(IOptions<DadosBase> dadosBase, IOptions<LoginRespostaModel> loginRespostaModel, IApiToken apiToken, IHttpClientFactory httpClient)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
            _apiToken = apiToken;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: FormasDePagamentoController
        public async Task<IActionResult> Index(string? mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}FormasDePagamento").Result;

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<List<FormasDePagamentoModel>>(response.Content.ReadAsStringAsync().Result));
            else
                throw new Exception("Não foi possível carregar as informações!");

        }

        // GET: FormasDePagamentoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: FormasDePagamentoController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: FormasDePagamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FormasDePagamentoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}FormasDePagamento", model).Result;

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

        // GET: FormasDePagamentoController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}FormasDePagamento/ObterDados?Id={id}").Result;

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<FormasDePagamentoModel>(response.Content.ReadAsStringAsync().Result));
            else
                throw new Exception("Não foi possível carregar as informações!");
        }

        // POST: FormasDePagamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] FormasDePagamentoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}FormasDePagamento", model).Result;

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

        // GET: FormasDePagamentoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: FormasDePagamentoController/Delete/5
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
