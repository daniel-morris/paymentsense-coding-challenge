using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICachedLookupService 
    {
        Task<string> GetJsonFromCacheOrDataSourceAsync(string cacheKey, string urlBase, string resource, string fields);
    }
}
