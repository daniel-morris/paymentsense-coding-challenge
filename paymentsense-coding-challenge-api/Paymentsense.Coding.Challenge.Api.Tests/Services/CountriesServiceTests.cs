using FluentAssertions;
using Moq;
using Paymentsense.Coding.Challenge.Api.Services;
using Paymentsense.Coding.Challenge.Api.Tests.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountriesServiceTests
    {
        private readonly ICountriesService _countriesService;
        private readonly Mock<ICachedLookupService> _mockCachedLookupService;
        private DummyCountriesData _dummyCountriesData;

        public CountriesServiceTests()
        {
            _dummyCountriesData = new DummyCountriesData();

            _mockCachedLookupService = new Mock<ICachedLookupService>();

            _countriesService = new CountriesService(_mockCachedLookupService.Object, "");
        }

        [Fact]
        public async Task GetAllAsync_ReturnsExpectedResult()
        {
            // Arrange
            _mockCachedLookupService.Setup(s => s.GetJsonFromCacheOrDataSourceAsync(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(_dummyCountriesData.CountriesJson));

            var expectedNumberofCountries = _dummyCountriesData.Countries.Count;

            // Act
            var countriesResult = await _countriesService.GetAllAsync();
            var countries = countriesResult.Data.ToList();

            // Assert
            countries.Count.Should().Be(expectedNumberofCountries);

            var firstCountry = countries[0];
            firstCountry.Name.Equals("Country A").Should().BeTrue();
            firstCountry.Code.Equals("AAA").Should().BeTrue();
            firstCountry.Flag.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllAsync_NoDataFound_ReturnsExpectedResult()
        {
            // Arrange
            _mockCachedLookupService.Setup(s => s.GetJsonFromCacheOrDataSourceAsync(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult("[]"));

            // Act
            var countriesResult = await _countriesService.GetAllAsync();

            // Assert
            countriesResult.Success.Should().BeFalse();
            countriesResult.Message.Equals("No data found.").Should().BeTrue();
            countriesResult.Data.Should().BeNull();
        }
    }
}
