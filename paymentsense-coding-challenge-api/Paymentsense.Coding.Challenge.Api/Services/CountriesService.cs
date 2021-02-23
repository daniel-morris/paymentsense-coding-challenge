using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountriesService : ICountriesService
    {
        protected readonly ICachedLookupService _cachedLookupService;
        protected readonly string _urlBase;

        public CountriesService(ICachedLookupService cachedLookupService, string urlBase)
        {
            _cachedLookupService = cachedLookupService;
            _urlBase = urlBase;
        }

        public async Task<IOperationResult<IEnumerable<Country>>> GetAllAsync()
        {
            var result = new OperationResult<IEnumerable<Country>> { Success = false, Message = "No data found." };

            var cacheKey = $"{_cacheKeyCountryPrefix}All";
            var resource = "all";
            var fieldsFilter = $"?fields={_countrySummaryPropertiesFilter}";
            var json = await _cachedLookupService.GetJsonFromCacheOrDataSourceAsync(cacheKey, _urlBase, resource, fieldsFilter);
            
            var allCountries = JsonSerializer.Deserialize<Country[]>(json);
            if (allCountries != null && allCountries.Length > 0)
            {
                result.Data = allCountries.ToList();
                SetResultSuccessAndEmptyMessage(result);
            }

            return result;
        }

        public async Task<IOperationResult<CountryDetails>> GetDetailsByCodeAsync(string code)
        {
            var result = new OperationResult<CountryDetails> { Success = false, Message = "Country code not specified." };
            if (string.IsNullOrWhiteSpace(code))
            {
                return result;
            }

            var cacheKey = $"{_cacheKeyCountryDetailsPrefix}{code.ToUpper()}";
            var resource = $"alpha/{code}";
            var fieldsFilter = $"?fields={_countryDetailsPropertiesFilter}";
            var json = await _cachedLookupService.GetJsonFromCacheOrDataSourceAsync(cacheKey, _urlBase, resource, fieldsFilter);

            // if not found, the endpoint returns a 404 json object
            if (json.Equals(_countryNotFoundResult, System.StringComparison.OrdinalIgnoreCase))
            {
                result.Message = "Country not found.";
                return result;
            }

            result.Data = JsonSerializer.Deserialize<CountryDetails>(json);
            SetResultSuccessAndEmptyMessage(result);
            return result;
        }

        public async Task<IOperationResult<IEnumerable<Country>>> GetByCodesAsync(string codes)
        {
            var result = new OperationResult<IEnumerable<Country>> { Success = false, Message = "Country codes not specified." };
            if (string.IsNullOrWhiteSpace(codes))
            {
                return result;
            }

            var cacheKey = $"{_cacheKeyCountryBordersPrefix}{codes.ToUpper()}";
            var resource = $"alpha/?codes={codes}";
            var fieldsFilter = $"&fields={_countrySummaryPropertiesFilter}";
            var json = await _cachedLookupService.GetJsonFromCacheOrDataSourceAsync(cacheKey, _urlBase, resource, fieldsFilter);
            
            // if not found the endpoint returns an array with a null value inside
            if (json.Equals(_nullArrayResult, System.StringComparison.OrdinalIgnoreCase))
            {
                result.Message = "Countries not found.";
                return result;
            }

            result.Data = JsonSerializer.Deserialize<IEnumerable<Country>>(json);
            SetResultSuccessAndEmptyMessage(result);
            return result;
        }

        private void SetResultSuccessAndEmptyMessage(IOperationResult result)
        {
            result.Success = true;
            result.Message = "";
        }

        private readonly string _countrySummaryPropertiesFilter = "name;alpha3Code;flag";
        private readonly string _countryDetailsPropertiesFilter = "name;alpha3Code;flag;capital;region;subregion;population;timezones;borders";

        private readonly string _countryNotFoundResult = "{\"status\":404,\"message\":\"Not Found\"}";
        private readonly string _nullArrayResult = "[null]";
        private readonly string _cacheKeyCountryPrefix = "Countries.";
        private readonly string _cacheKeyCountryDetailsPrefix = "Countries.Detail.";
        private readonly string _cacheKeyCountryBordersPrefix = "Countries.Borders.";
    }
}
