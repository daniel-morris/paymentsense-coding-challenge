using System.Collections.Generic;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountriesService
    {
        Task<IOperationResult<IEnumerable<Country>>> GetAllAsync();
        Task<IOperationResult<CountryDetails>> GetDetailsByCodeAsync(string code);
        Task<IOperationResult<IEnumerable<Country>>> GetByCodesAsync(string codes);
    }
}
