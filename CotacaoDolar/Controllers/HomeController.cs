using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CotacaoDolar.Models;
using System.Net.Http;
using System.Text.Json;

namespace CotacaoDolar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var url = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{DateTime.Today.AddDays(-1).ToString("MM-dd-yyyy")}'&$top=100&$format=json";
            var request = new HttpRequestMessage(HttpMethod.Get,url);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);
            var cotacaoMoeda = new CotacaoMoeda();
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ResultBC>(responseStream);
                cotacaoMoeda.CotacaoVenda = (long) (result.Value.FirstOrDefault().CotacaoVenda * 10000);
                cotacaoMoeda.CotacaoCompra = (long) (result.Value.FirstOrDefault().CotacaoCompra * 10000);
            }
            return View(cotacaoMoeda);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
