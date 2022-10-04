using CarLocadora.Comum.Modelo;
using CarLocadora.Comum.Servico;
using CarLocadora.Modelo.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarLocadora.Front.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly string mensagem = String.Empty;

        private readonly IOptions<DadosBase> _dadosBase;
        private readonly IOptions<LoginRespostaModel> _loginRespostaModel;
        private readonly HttpClient _httpClient;
        private readonly IApiToken _apiToken;

        public LocacaoController(IOptions<DadosBase> dadosBase, IOptions<LoginRespostaModel> loginRespostaModel, IApiToken apiToken, IHttpClientFactory httpClient)
        {
            _dadosBase = dadosBase;
            _loginRespostaModel = loginRespostaModel;
            _apiToken = apiToken;
            _httpClient = httpClient.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: LocacoesController
        public async Task<IActionResult> Index(string? mensagem = null, bool sucesso = true)
        {
            if (sucesso)
                TempData["sucesso"] = mensagem;
            else
                TempData["erro"] = mensagem;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Locacao").Result;

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<List<LocacoesModel>>(response.Content.ReadAsStringAsync().Result));
            else
                throw new Exception("Não foi possível carregar as informações!");
        }

        // GET: LocacoesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }

        // GET: LocacoesController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: LocacoesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] LocacoesModel model)
        {
            try
            {
                ViewBag.Veiculos = CarregarVeiculos().Result;
                ViewBag.Clientes = CarregarClientes().Result;
                ViewBag.FormasDePagamento = CarregarFormasDePagamento().Result;

                return View();

            }
            catch (Exception ex)
            {
                TempData["erro"] = "Algum erro aconteceu " + ex.Message;
                return View();
            }
        }

        // GET: LocacoesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Locacao/ObterDados?Id={id}");

            if (response.IsSuccessStatusCode)
                return View(JsonConvert.DeserializeObject<LocacoesModel>(await response.Content.ReadAsStringAsync()));
            else
                throw new Exception("Não foi possível carregar as informações!");
        }

        // POST: LocacoesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] LocacoesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{_dadosBase.Value.API_URL_BASE}Locacao", model);

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

        // GET: LocacoesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: LocacoesController/Delete/5
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


        private async Task<List<SelectListItem>> CarregarVeiculos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Veiculo");

            if (response.IsSuccessStatusCode)
            {
                List<VeiculoModel> veiculos = JsonConvert.DeserializeObject<List<VeiculoModel>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in veiculos)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Placa,
                        Text = linha.Modelo + " - " + linha.Marca,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        private async Task<List<SelectListItem>> CarregarClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}Cliente");

            if (response.IsSuccessStatusCode)
            {
                List<ClienteModel> Clientes = JsonConvert.DeserializeObject<List<ClienteModel>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in Clientes)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.CPF,
                        Text = linha.Nome + " - " + linha.CPF,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        private async Task<List<SelectListItem>> CarregarFormasDePagamento()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _apiToken.Obter());
            HttpResponseMessage response = await _httpClient.GetAsync($"{_dadosBase.Value.API_URL_BASE}FormasDePagamento");

            if (response.IsSuccessStatusCode)
            {
                List<FormasDePagamentoModel> formasPagamento = JsonConvert.DeserializeObject<List<FormasDePagamentoModel>>(await response.Content.ReadAsStringAsync());

                foreach (var linha in formasPagamento)
                {
                    lista.Add(new SelectListItem()
                    {
                        Value = linha.Id.ToString(),
                        Text = linha.Descricao,
                        Selected = false,
                    });
                }

                return lista;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
