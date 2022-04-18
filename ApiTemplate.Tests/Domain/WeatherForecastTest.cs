using System;
using ApiTemplate.Domain.Entities;
using Xunit;

namespace ApiTemplate.Tests.Domain;

public class WeatherForecastTest
{
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
                    && weatherForecast.TemperatureC == 10
                    && !string.IsNullOrEmpty(weatherForecast.Summary));
    }
}