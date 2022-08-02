using System;
using ApiTemplate.Domain.Entities;
using Xunit;

namespace ApiTemplate.Tests.DomainProjectTest;

public class WeatherForecastTest
{
    private const double Tolerance = 1e-6;

    [Fact]
    public void ShouldBeAbleToCreateAWeatherForecast()
    {
        var weatherForecast = new WeatherForecast
        {
            Id = Guid.Empty,
            Date = default,
            TemperatureC = 0,
            Summary = null
        };
        Assert.NotNull(weatherForecast);
    }

    [Fact]
    public void ShouldBeAbleToCreateAWeatherForecastWithId()
    {
        var weatherForecast = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            TemperatureC = 10,
            Summary = "Test summary"
        };
        Assert.True(weatherForecast.Id != Guid.Empty
                    && weatherForecast.Date != default
                    && Math.Abs(weatherForecast.TemperatureC - 10) < Tolerance
                    && !string.IsNullOrEmpty(weatherForecast.Summary));
    }
}
