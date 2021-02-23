using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Paymentsense.Coding.Challenge.Api.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Paymentsense.Coding.Challenge.Api.Tests.Data;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class CountryControllerTests 
    {
        private DummyCountriesData _dummyCountriesData;
        private CountryController _controller;
        private Mock<ICountriesService> _mockCountriesService;

        public CountryControllerTests()
        {
            _dummyCountriesData = new DummyCountriesData();
            _mockCountriesService = new Mock<ICountriesService>();
            _controller = new CountryController(_mockCountriesService.Object);
        }

        [Fact]
        public void Get_OnInvoke_ReturnsExpectedData()
        {
            // Arrange
            var operationResult 
                = new OperationResult<IEnumerable<Country>> 
                { Success = true, Message = "", Data = _dummyCountriesData.Countries }
                as IOperationResult<IEnumerable<Country>>;
            _mockCountriesService.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(operationResult));

            // Act
            var result = _controller.Get().Result as OkObjectResult;
            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be(operationResult);
        }

        [Fact]
        public void Get_WithCodeParameter_OnInvoke_ReturnsExpectedData()
        {
            // Arrange
            var operationResult
                = new OperationResult<CountryDetails>
                { Success = true, Message = "", Data = _dummyCountriesData.CountryDetails } as IOperationResult<CountryDetails>;
            _mockCountriesService.Setup(x => x.GetDetailsByCodeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(operationResult));

            const string code = "AAA";

            // Act
            var result = _controller.Get(code).Result as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be(operationResult);
        }

        [Fact]
        public void Get_WithCodesParameter_OnInvoke_ReturnsExpectedData()
        {
            // Arrange
            var operationResult = new OperationResult<IEnumerable<Country>>
            { Success = true, Message = "", Data = _dummyCountriesData.Borders } as IOperationResult<IEnumerable<Country>>;
            _mockCountriesService.Setup(x => x.GetByCodesAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(operationResult));

            const string codes = "CCC;DDD";

            // Act
            var result = _controller.GetByCodes(codes).Result as OkObjectResult;

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().Be(operationResult);
        }
    }
}
