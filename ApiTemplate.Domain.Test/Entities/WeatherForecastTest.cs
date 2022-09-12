using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Domain.Test.Entities;

public class WeatherForecastTest
{
    [Fact]
    public void ShouldBeAbleToCreateAWeatherForecast()
    {
        var weatherForecast = new WeatherForecast(Guid.Empty, default, 0, default);

        Assert.NotNull(weatherForecast);
    }

    [Fact]
    public void ShouldBeAbleToCreateAWeatherForecastWithId()
    {
        var weatherForecast = new WeatherForecast(Guid.Empty, DateTime.Now, 10, "Test summary");

        Assert.True(weatherForecast.Id != Guid.Empty
                    && weatherForecast.Date != default
                    && weatherForecast.TemperatureCelsius == 10
                    && !string.IsNullOrEmpty(weatherForecast.Summary));
    }
}
