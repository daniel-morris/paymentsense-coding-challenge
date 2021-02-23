using Microsoft.Extensions.Caching.Distributed;
using Paymentsense.Coding.Challenge.Api.Handlers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CachedLookupService : ICachedLookupService
    {
        protected readonly IHttpCallsHandler _httpCallsHandler;
        protected readonly IDistributedCache _cache;
        public CachedLookupService(IHttpCallsHandler  httpCallsHandler, IDistributedCache cache)
        {
            _httpCallsHandler = httpCallsHandler;
            _cache = cache;
        }

        public async Task<string> GetJsonFromCacheOrDataSourceAsync(string cacheKey, string urlBase, string resource, string fields)
        {
            var jsonBytes = await _cache.GetAsync(cacheKey);
            if (jsonBytes == null)
            {
                string url = $"{ urlBase }{ resource }{ fields }";
                var json = await _httpCallsHandler.GetAsync(url);
                jsonBytes = Encoding.UTF8.GetBytes(json);
                await _cache.SetAsync(cacheKey, jsonBytes);
            }
            return Encoding.UTF8.GetString(jsonBytes);
        }
    }
}
