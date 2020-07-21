using CotacaoDolar.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CotacaoDolar.Infra
{
    public class CotacaoRequest
    {
        private readonly IHttpClientFactory _clientFactory;

        public CotacaoRequest(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CotacaoMoeda> GetCotacao(DateTime? date = null)
        {
            date ??= DateTime.Today;
            using var request = new HttpRequestMessage(HttpMethod.Get, GetUrl(date.Value));
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            var cotacaoMoeda = new CotacaoMoeda();
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ResultBC>(responseStream);
                if (!result.Value.Any())
                {
                    return await GetCotacao(date.Value.AddDays(-1));
                }
                cotacaoMoeda.DataFechamentoCotacao = date.Value;
                cotacaoMoeda.Cotacao = (long)(result.Value.FirstOrDefault().CotacaoCompra * 10000);
            }
            return cotacaoMoeda;
        }

        private static string GetUrl(DateTime date) =>
            $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoDolarDia(dataCotacao=@dataCotacao)?@dataCotacao='{date:MM-dd-yyyy}'&$top=100&$format=json";
    }
}