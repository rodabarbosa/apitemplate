using System;
using ApiTemplate.Application.Models;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Models;

public class WeatherForecastDtoTest
{
    [Fact]
    public void WeatherForecastDto_Should_Be_Created()
    {
        var weatherForecastDto = new WeatherForecastDto
        {
            Date = DateTime.Now,
            TemperatureC = 1,
            Summary = "Summary"
        };

        Assert.NotNull(weatherForecastDto);
    }

    [Fact]
    public void WeatherForecastDto_Should_Be_TemperatureCheck()
    {
        var weatherForecastDto = new WeatherForecastDto
        {
            Date = DateTime.Now,
            TemperatureC = 0,
            Summary = "Summary"
        };

        Assert.Equal(0, weatherForecastDto.TemperatureC);
        Assert.Equal(32, weatherForecastDto.TemperatureF);
    }
}
