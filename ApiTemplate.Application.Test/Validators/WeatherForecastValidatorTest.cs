using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Validators;
using ApiTemplate.Shared.Extensions;

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
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData]
    public void GivenAWeatherForecast_WhenValidating_ThenValidationShouldFail()
    {
        var weatherForecast1 = new UpdateWeatherForecastRequestContract
        {
            Id = default,
            Date = DateTime.Now.AddHours(-1),
            TemperatureCelsius = 30,
            TemperatureFahrenheit = 150,
            Summary = default
        };

        var result1 = _validator.Validate(weatherForecast1);
        Assert.False(result1.IsValid);

        var weatherForecast2 = new UpdateWeatherForecastRequestContract
        {
            Id = default,
            Date = DateTime.Now.AddHours(-1),
            TemperatureCelsius = default,
            TemperatureFahrenheit = 150,
            Summary = default
        };

        var result2 = _validator.Validate(weatherForecast2);
        Assert.False(result2.IsValid);

        var weatherForecast3 = new UpdateWeatherForecastRequestContract
        {
            Id = default,
            Date = DateTime.Now.AddHours(-1),
            TemperatureCelsius = 30,
            TemperatureFahrenheit = default,
            Summary = default
        };

        var result3 = _validator.Validate(weatherForecast3);
        Assert.False(result3.IsValid);
    }
}
