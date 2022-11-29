using CarLocadora.Comum.Modelos;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Front.Controllers
{
    public class VistoriaController : Controller
    {
        private string mensagem = String.Empty;

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginResposta> _loginRespostaModel;
        private readonly IApiToken _apiToken;
        private readonly HttpClient _httpClient;
        public VistoriaController(IOptions<DadosBase> dadosBase, IOptions<LoginResposta> loginRespostaModel, IApiToken apiToken, IHttpClientFactory httpClient)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
            _apiToken = apiToken;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: VistoriasController
        public async Task<IActionResult> Index(string? mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Vistoria").Result;

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<List<VistoriasModel>>(response.Content.ReadAsStringAsync().Result));
            else
                throw new Exception("Não foi possível carregar as informações!");
        }

        // GET: VistoriasController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: VistoriasController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VistoriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] VistoriasModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = _httpClient.PostAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Vistoria", model).Result;

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

        // GET: VistoriasController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Vistoria/ObterDados?Id={id}").Result;

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<VistoriasModel>(response.Content.ReadAsStringAsync().Result));
            else
                throw new Exception("Não foi possível carregar as informações!");
        }

        // POST: VistoriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] VistoriasModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Vistoria", model).Result;

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

        // GET: VistoriasController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: VistoriasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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
