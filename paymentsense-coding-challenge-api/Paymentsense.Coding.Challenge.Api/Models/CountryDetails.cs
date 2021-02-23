using System.Text.Json.Serialization;
namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class CountryDetails : Country
    {
        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("subregion")]
        public string Subregion { get; set; }

        [JsonPropertyName("population")]
        public int? Population { get; set; }

        [JsonPropertyName("timezones")]       
        public string[] Timezones { get; set; }

        [JsonPropertyName("borders")]
        public string[] Borders { get; set; }
    }
}