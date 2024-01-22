namespace ApiTemplate.Application.Test.Validators;

public class WeatherForecastValidatorTest
{
    private readonly UpdateWeatherForecastValidator _validator = new();

    [Fact]
    public void GivenAWeatherForecast_WhenValidating_ThenValidationShouldPass()
    {
        // Arrange
        var celsius = 30m;
        var weatherForecast = new UpdateWeatherForecastRequestContract
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now.AddHours(-1),
            TemperatureCelsius = celsius,
            TemperatureFahrenheit = celsius.ToFahrenheit(),
            Summary = default
        };

        // Act
        var result = _validator.Validate(weatherForecast);

        // Assert
        result.IsValid
            .Should()
            .BeTrue();
    }

    [Theory]
    [InlineData(-1, 250, 150)]
    [InlineData(-1, 0, 5000)]
    [InlineData(10, 220, 0)]
    public void Should_Return_False_Validation(int removeFromHours, decimal temperatureCelsius, decimal temperatureFahrenheit)
    {
        var weather = new UpdateWeatherForecastRequestContract
        {
            Id = default,
            Date = DateTime.Now.AddHours(removeFromHours),
            TemperatureCelsius = temperatureCelsius,
            TemperatureFahrenheit = temperatureFahrenheit,
            Summary = default
        };

        var result = _validator.Validate(weather);

        result.IsValid
            .Should()
            .BeFalse();
    }
}
