namespace ApiTemplate.Shared.Test.Exceptions;

public class ValidationExceptionTest
{
    private readonly CreateWeatherForecastValidator _validator = new();

    [Fact]
    public void ValidationException_Should_Be_Created()
    {
        // arrange

        // Act
        var exception = new ValidationException(Enumerable.Empty<ValidationFailure>());

        // Assert
        exception.Should()
            .NotBeNull();
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
        exception.InnerException!
            .Message
            .Should()
            .Be(message);
    }

    [Fact]
    public void ValidationException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "One or more validation failure have occurred.";

        // Act
        var exception = new ValidationException(Enumerable.Empty<ValidationFailure>());

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    public void Condition_Throws_Exception(bool condition, string message)
    {
        var act = () => ValidationException.ThrowIf(condition, message);

        act.Should()
            .Throw<ValidationException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Condition_NotThrows_Exception(bool condition, string message)
    {
        var act = () => ValidationException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }

    [Theory]
    [InlineData(10, 2500)]
    [InlineData(-1, 250)]
    public void Should_Throw_Exception_Using_ThrowIf(int daysToAdd, decimal temperature)
    {
        var weather = new CreateWeatherForecastRequestContract
        {
            Date = DateTime.Now.AddDays(daysToAdd),
            TemperatureCelsius = temperature,
            Summary = "Test summary"
        };

        var result = _validator.Validate(weather);

        var act = () => ValidationException.ThrowIf(!result.IsValid, result.Errors);

        act.Should()
            .Throw<ValidationException>();
    }

    [Theory]
    [InlineData(-1, 20)]
    public void Should_Not_Throw_Exception_Using_ThrowIf(int daysToAdd, decimal temperature)
    {
        var weather = new CreateWeatherForecastRequestContract
        {
            Date = DateTime.Now.AddDays(daysToAdd),
            TemperatureCelsius = temperature,
            Summary = "Test summary"
        };

        var result = _validator.Validate(weather);

        var act = () => ValidationException.ThrowIf(!result.IsValid, result.Errors);

        act.Should()
            .NotThrow();
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
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (ValidationException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
