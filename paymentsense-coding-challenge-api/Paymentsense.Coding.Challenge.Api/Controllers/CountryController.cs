using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [Route("country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountriesService _countriesService;
        public CountryController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }
        
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _countriesService.GetAllAsync();
            return Ok(result);
        }

        [Route("list/codes/{codes}")]
        [HttpGet]
        public async Task<IActionResult> GetByCodes(string codes)
        {
            var result = await _countriesService.GetByCodesAsync(codes);
            return Ok(result);
        }

        [Route("detail/{code}")]
        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            var result = await _countriesService.GetDetailsByCodeAsync(code);
            return Ok(result);
        }
    }
}
