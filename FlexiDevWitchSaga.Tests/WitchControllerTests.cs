using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FlexiDevWitchSaga.Server.Controllers;
using FlexiDevWitchSaga.Server.Models;
using FlexiDevWitchSaga.Server.Services;

namespace FlexiDevWitchSaga.Tests
{
    public class WitchControllerTests
    {
        private readonly Mock<IWitchKillCalculator> _mockCalculator;
        private readonly WitchController _controller;

        public WitchControllerTests()
        {
            _mockCalculator = new Mock<IWitchKillCalculator>();
            _controller = new WitchController(_mockCalculator.Object);
        }

        [Fact]
        public void CalculateAverageKilled_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var people = new Person[]
            {
                new Person { AgeOfDeath = 10, YearOfDeath = 12 },
                new Person { AgeOfDeath = 13, YearOfDeath = 17 }
            };
            var expectedResult = new KillCalculationResult { AverageKilled = 4.5, IsValid = true };
            _mockCalculator.Setup(c => c.CalculateAverageKilled(It.IsAny<Person>(), It.IsAny<Person>()))
                .Returns(expectedResult);

            // Act
            var actionResult = _controller.CalculateAverageKilled(people);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<KillCalculationResult>(okResult.Value);
            Assert.Equal(expectedResult.AverageKilled, returnValue.AverageKilled);
            Assert.Equal(expectedResult.IsValid, returnValue.IsValid);
        }

        [Fact]
        public void CalculateAverageKilled_InvalidNumberOfPeople_ReturnsBadRequest()
        {
            // Arrange
            var people = new Person[]
            {
                new Person { AgeOfDeath = 10, YearOfDeath = 12 }
            };

            // Act
            var actionResult = _controller.CalculateAverageKilled(people);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
    }
}
