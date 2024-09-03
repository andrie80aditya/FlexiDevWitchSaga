using System;
using Xunit;
using FlexiDevWitchSaga.Server.Models;
using FlexiDevWitchSaga.Server.Services;

namespace FlexiDevWitchSaga.Tests
{
    public class WitchKillCalculatorTests
    {
        private readonly WitchKillCalculator _calculator;

        public WitchKillCalculatorTests()
        {
            _calculator = new WitchKillCalculator();
        }

        [Fact]
        public void CalculateAverageKilled_ValidInput_ReturnsCorrectResult()
        {
            // Arrange
            var personA = new Person { AgeOfDeath = 10, YearOfDeath = 12 };
            var personB = new Person { AgeOfDeath = 13, YearOfDeath = 17 };

            // Act
            var result = _calculator.CalculateAverageKilled(personA, personB);

            // Assert
            Assert.True(result.IsValid);
            Assert.Equal(4.5, result.AverageKilled);
        }

        [Fact]
        public void CalculateAverageKilled_InvalidInput_ReturnsNegativeOne()
        {
            // Arrange
            var personA = new Person { AgeOfDeath = -5, YearOfDeath = 12 };
            var personB = new Person { AgeOfDeath = 13, YearOfDeath = 17 };

            // Act
            var result = _calculator.CalculateAverageKilled(personA, personB);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(-1, result.AverageKilled);
        }

        [Fact]
        public void CalculateAverageKilled_BornBeforeWitchControl_ReturnsNegativeOne()
        {
            // Arrange
            var personA = new Person { AgeOfDeath = 15, YearOfDeath = 10 };
            var personB = new Person { AgeOfDeath = 13, YearOfDeath = 17 };

            // Act
            var result = _calculator.CalculateAverageKilled(personA, personB);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(-1, result.AverageKilled);
        }
    }
}