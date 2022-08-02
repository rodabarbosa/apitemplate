using System;
using System.Linq;
using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Services;

public sealed class WeatherForecastServiceTest : IDisposable
{
    private readonly ApiTemplateContext _context;
    private readonly WeatherForecastService _weatherForecastService;

    public WeatherForecastServiceTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApiTemplateContext>()
            .UseInMemoryDatabase("ApiTemplate");
        _context = new ApiTemplateContext(optionsBuilder.Options);

        var weatherForecastRepository = new WeatherForecastRepository(_context);
        _weatherForecastService = new WeatherForecastService(weatherForecastRepository);
    }

    [Fact]
    public void GetForecast_Returns_WeatherForecast_collection_And_By_Id()
    {
        // Act
        var result = _weatherForecastService.GetWeatherForecasts(null, null, null);

        // Assert
        var weatherForecastDtos = result as WeatherForecastDto[] ?? result.ToArray();
        Assert.NotEmpty(weatherForecastDtos);

        var single = _weatherForecastService.GetWeatherForecast(weatherForecastDtos.First().Id!.Value);
        Assert.NotNull(single);
    }

    [Fact]
    public void GetForecast_Returns_WeatherForecast_With_Filter()
    {
        // Act

        var result = _weatherForecastService.GetWeatherForecasts(new OperationParam<DateTime>
        {
            Operation = Operation.Equal,
            Value = DateTime.Now.AddDays(1)
        }, null, null);

        // Assert
        Assert.Empty(result);

        result = _weatherForecastService.GetWeatherForecasts(null, new OperationParam<double>
        {
            Operation = Operation.Equal,
            Value = 300
        }, null);

        // Assert
        Assert.Empty(result);

        result = _weatherForecastService.GetWeatherForecasts(null, null,
            new OperationParam<double>
            {
                Operation = Operation.Equal,
                Value = 3000
            });

        // Assert
        Assert.Empty(result);

        result = _weatherForecastService.GetWeatherForecasts(new OperationParam<DateTime>
            {
                Operation = Operation.Equal,
                Value = DateTime.Now.AddDays(1)
            },
            new OperationParam<double>
            {
                Operation = Operation.Equal,
                Value = 300
            },
            null);

        // Assert
        Assert.Empty(result);

        result = _weatherForecastService.GetWeatherForecasts(new OperationParam<DateTime>
            {
                Operation = Operation.Equal,
                Value = DateTime.Now.AddDays(1)
            },
            null,
            new OperationParam<double>
            {
                Operation = Operation.Equal,
                Value = 3000
            });

        // Assert
        Assert.Empty(result);

        result = _weatherForecastService.GetWeatherForecasts(new OperationParam<DateTime>
            {
                Operation = Operation.Equal,
                Value = DateTime.Now.AddDays(1)
            },
            new OperationParam<double>
            {
                Operation = Operation.Equal,
                Value = 300
            },
            new OperationParam<double>
            {
                Operation = Operation.Equal,
                Value = 3000
            });

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WeatherForecast_Creating()
    {
        // Arrange
        var count = _weatherForecastService.GetWeatherForecasts(null, null, null).Count();

        var weatherForecast = new WeatherForecastDto
        {
            Date = DateTime.Now,
            TemperatureC = 10,
            Summary = "Summary"
        };

        // Act
        _weatherForecastService.AddWeatherForecast(weatherForecast);

        var newCount = _weatherForecastService.GetWeatherForecasts(null, null, null).Count();
        // Assert
        Assert.NotEqual(count, newCount);
    }

    [Fact]
    public void WeatherForecast_Updating()
    {
        // Arrange

        var weathers = _weatherForecastService.GetWeatherForecasts(null, null, null);
        var weather = weathers.First();

        weather.TemperatureC = 20;
        weather.Summary = "Summary 1";

        // Act
        _weatherForecastService.UpdateWeatherForecast(weather.Id!.Value, weather);

        // Assert
        Assert.Equal(weather.TemperatureC, _weatherForecastService.GetWeatherForecast(weather.Id.Value).TemperatureC);
    }

    [Fact]
    public void WeatherForecast_Deleting()
    {
        // Arrange

        var weathers = _weatherForecastService.GetWeatherForecasts(null, null, null);
        var weather = weathers.First();

        // Act
        _weatherForecastService.DeleteWeatherForecast(weather.Id!.Value);

        // Assert
        Assert.Null(_weatherForecastService.GetWeatherForecast(weather.Id.Value));
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
