using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Validators;
using ApiTemplate.Shared.Exceptions;
using FluentValidation.Results;
using Xunit;

namespace ApiTemplate.Tests.Shared.Exceptions;

public class ValidationExceptionTest
{
    [Fact]
    public void ValidationException_Should_Be_Created()
    {
        // arrange

        // Act
        var exception = new ValidationException(Enumerable.Empty<ValidationFailure>());

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void ValidationException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var message = "Test Exception";
        var expectedInnerException = new Exception(message);

        // Act
        var exception = new ValidationException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException.Message, exception.InnerException.Message);
    }

    [Fact]
    public void ValidationException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "One or more validation failure have occurred.";

        // Act
        var exception = new ValidationException(Enumerable.Empty<ValidationFailure>());

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void ValidationException_When_Meets_Condition(bool condition, string message)
    {
        // Arrange

        if (condition)
        {
            Assert.Throws<ValidationException>(() => { ValidationException.When(condition, message); });
        }
        else
        {
            ValidationException.When(condition, message);
            Assert.True(!condition);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ValidationException_When_UsingValidator(int index)
    {
        var validator = new WeatherForecastValidator();
        switch (index)
        {
            case 1:
                var weather1 = new WeatherForecastDto
                {
                    Id = default,
                    Date = DateTime.Now.AddDays(10),
                    TemperatureC = 2000,
                    Summary = "Test summary"
                };

                var result1 = validator.Validate(weather1);
                Assert.Throws<ValidationException>(() => { ValidationException.When(!result1.IsValid, result1.Errors); });
                break;

            case 2:
                var weather2 = new WeatherForecastDto
                {
                    Id = default,
                    Date = DateTime.Now.AddDays(-1),
                    TemperatureC = 20,
                    Summary = "Test summary"
                };

                var result = validator.Validate(weather2);
                Assert.True(result.IsValid);
                break;

            case 3:
                Assert.Throws<ValidationException>(() => { ValidationException.When(true, Enumerable.Empty<ValidationFailure>()); });
                break;
        }
    }

    [Fact]
    public void ValidationException_Serialization()
    {
        // Arrange
        var expectedMessage = "One or more validation failure have occurred.";
        var e = new ValidationException(Enumerable.Empty<ValidationFailure>());

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (ValidationException) formatter.Deserialize(s);
        }

        // Assert
        Assert.Equal(expectedMessage, e.Message);
    }
}