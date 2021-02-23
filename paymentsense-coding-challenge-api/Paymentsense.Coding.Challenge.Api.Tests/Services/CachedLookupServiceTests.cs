using System.Text;
using FluentAssertions;
using Paymentsense.Coding.Challenge.Api.Tests.Data;
using Paymentsense.Coding.Challenge.Api.Services;
using Moq;
using System.Net.Http;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Paymentsense.Coding.Challenge.Api.Handlers;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CachedLookupServiceTests
    {
        private CachedLookupService _cachedLookupService;
        private Mock<IHttpCallsHandler> _mockHttpCallsHandler;
        private Mock<IDistributedCache> _mockDistributedCache;
        private DummyCountriesData _dummyCountriesData;

        public CachedLookupServiceTests()
        {
            _mockHttpCallsHandler = new Mock<IHttpCallsHandler>();
            _mockDistributedCache = new Mock<IDistributedCache>();
            _dummyCountriesData = new DummyCountriesData();
            _cachedLookupService = new CachedLookupService(_mockHttpCallsHandler.Object, _mockDistributedCache.Object);
        }

        [Fact]
        public async Task GetJsonFromCacheOrDataSourceAsync_ReturnsCachedValue() 
        {
            // Arrange
            _mockHttpCallsHandler.Setup(f => f.GetAsync(It.IsAny<string>())).Verifiable();
            _mockDistributedCache.Setup(c => c.GetAsync(It.IsAny<string>(), default))
                .Returns(Task.FromResult(Encoding.UTF8.GetBytes(_dummyCountriesData.CountriesJson)));
            
            // Act
            var result = await _cachedLookupService.GetJsonFromCacheOrDataSourceAsync("", "", "", "");

            // Assert
            result.Equals(_dummyCountriesData.CountriesJson).Should().BeTrue();
            _mockHttpCallsHandler.Verify(f => f.GetAsync(It.IsAny<string>()), Times.Never, "HttpCallsHandlershould not have been used, for cached item");
        }

        [Fact]
        public async Task GetJsonFromCacheOrDataSourceAsync_GetsValueFromService_CachesValues_ReturnsValue()
        {
            // Arrange
            
            _mockDistributedCache.Setup(c => c.GetAsync(It.IsAny<string>(), default))
                .Returns(Task.FromResult(null as byte[]));
            _mockDistributedCache.Setup(c => c.SetAsync(It.IsAny<string>(),It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default))
                .Verifiable();
            _mockHttpCallsHandler.Setup(h => h.GetAsync(It.IsAny<string>())).Returns(Task.FromResult(_dummyCountriesData.CountriesJson));
            var c = Encoding.UTF8.GetBytes(_dummyCountriesData.CountriesJson);

            // Act
            var result = await _cachedLookupService.GetJsonFromCacheOrDataSourceAsync("test-key", "", "", "");

            // Assert
            result.Equals(_dummyCountriesData.CountriesJson).Should().BeTrue();
            
            _mockDistributedCache.Verify(c => c.GetAsync(It.IsAny<string>(), default), Times.Once);
            _mockDistributedCache.Verify(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), 
                Times.Once, "Retrieved value, should have been saved to cache");

            _mockHttpCallsHandler.Verify(h => h.GetAsync(It.IsAny<string>()), Times.Once, "Http call should have been attempted to get data not found in cache");
        }
    }
}
