using System;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using ApiTemplate.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiTemplate.Tests.WebApiProjectTest.Controllers;

public class WeatherForecastControllerTest : IDisposable
{
    private readonly WeatherForecastController _controller;
    private readonly ApiTemplateContext _context;

    private readonly Guid _updatingWeather;
    private readonly Guid _deletingWeather;

    public WeatherForecastControllerTest()
    {
        _context = ContextUtil.GetContext();
        var weatherForecastRepository = new WeatherForecastRepository(_context);
        IWeatherForecastService weatherForecastService = new WeatherForecastService(weatherForecastRepository);
        _controller = new WeatherForecastController(weatherForecastService);

        _updatingWeather = weatherForecastRepository.Get().First().Id;
        _deletingWeather = weatherForecastRepository.Get().Last().Id;
    }

    [Fact]
    public async Task Get_Returns_Weather_Forecast()
    {
        // Act
        var result = await _controller.Get(null!);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task Get_Returns_Weather_Forecast_By_Id()
    {
        // Act
        var result = await _controller.Get(_updatingWeather);

        // Assert
        Assert.IsType<WeatherForecastDto>(result.Value);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task Post_Weather_Forecast()
    {
        // Act
        var dto = new WeatherForecastDto
        {
            Id = default,
            Date = DateTime.Now,
            TemperatureC = 10,
            Summary = null
        };

        var result = await _controller.Post(dto);

        // Assert
        Assert.IsType<WeatherForecastDto>(result.Value);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task Put_Weather_Forecast()
    {
        // Act
        var dto = new WeatherForecastDto
        {
            Id = _updatingWeather,
            Date = DateTime.Now.AddDays(-1),
            TemperatureC = 10,
            Summary = null
        };

        var result = await _controller.Put(_updatingWeather, dto);

        // Assert
        Assert.IsType<WeatherForecastDto>(result.Value);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task Delete_Weather_Forecast()
    {
        // Act
        await _controller.Delete(_deletingWeather);

        // Assert
        Assert.True(true);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
