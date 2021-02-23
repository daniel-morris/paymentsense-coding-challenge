using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Handlers
{
    public interface IHttpCallsHandler
    {
        Task<string> GetAsync(string url);
    }
}
