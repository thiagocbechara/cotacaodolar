using System.Text.Json.Serialization;

namespace CotacaoDolar.Models
{
    public class ResultBC
    {
        [JsonPropertyName("value")]
        public CotacaoBC[] Value { get; set; }
    }

    public class CotacaoBC
    {
        [JsonPropertyName("cotacaoCompra")]
        public decimal CotacaoCompra { get; set; }
        [JsonPropertyName("cotacaoVenda")]
        public decimal CotacaoVenda { get; set; }
    }
}