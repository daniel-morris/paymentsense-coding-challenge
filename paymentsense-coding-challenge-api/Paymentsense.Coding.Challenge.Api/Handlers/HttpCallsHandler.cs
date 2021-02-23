using System.Net.Http;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Handlers
{
    public class HttpCallsHandler : IHttpCallsHandler
    {
        protected readonly IHttpClientFactory _httpFactory;

        public HttpCallsHandler(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
        }

        public async Task<string> GetAsync(string url)
        {
            var client = _httpFactory.CreateClient("client");
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
