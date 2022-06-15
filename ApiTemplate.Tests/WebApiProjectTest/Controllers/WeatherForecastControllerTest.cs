using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using ApiTemplate.WebApi.Controllers;
using Xunit;

namespace ApiTemplate.Tests.WebApiProjectTest.Controllers;

public class WeatherForecastControllerTest
{
    private readonly WeatherForecastController _controller;
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly WeatherForecastRepository _weatherForecastRepository;
    private readonly ApiTemplateContext _context;

    private readonly Guid _updatingWeather;
    private readonly Guid _deletingWeather;

    public WeatherForecastControllerTest()
    {
        _context = ContextUtil.GetContext();
        _weatherForecastRepository = new WeatherForecastRepository(_context);
        _weatherForecastService = new WeatherForecastService(_weatherForecastRepository);
        _controller = new WeatherForecastController(_weatherForecastService);

        _updatingWeather = _weatherForecastRepository.Get().First().Id;
        _deletingWeather = _weatherForecastRepository.Get().Last().Id;
    }

    [Fact]
    public void Get_Returns_Weather_Forecast()
    {
        // Act
        var result = _controller.Get(null!);
        var data = result.Result.Value;

        // Assert
        Assert.IsAssignableFrom<IEnumerable<WeatherForecastDto>>(data);
        Assert.NotEmpty(data);
    }

    [Fact]
    public void Get_Returns_Weather_Forecast_By_Id()
    {
        // Act
        var result = _controller.Get(_updatingWeather);

        // Assert
        Assert.IsType<WeatherForecastDto>(result);
        Assert.NotNull(result);
    }

    [Fact]
    public void Post_Weather_Forecast()
    {
        // Act
        var dto = new WeatherForecastDto
        {
            Id = default,
            Date = DateTime.Now,
            TemperatureC = 10,
            Summary = null
        };

        var result = _controller.Post(dto);

        // Assert
        Assert.IsType<WeatherForecastDto>(result);
        Assert.NotNull(result);
    }

    [Fact]
    public void Put_Weather_Forecast()
    {
        // Act
        var dto = new WeatherForecastDto
        {
            Id = _updatingWeather,
            Date = DateTime.Now.AddDays(-1),
            TemperatureC = 10,
            Summary = null
        };

        var result = _controller.Put(_updatingWeather, dto);

        // Assert
        Assert.IsType<WeatherForecastDto>(result);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Delete_Weather_Forecast()
    {
        // Act
        await _controller.Delete(_deletingWeather);

        // Assert
        Assert.True(true);
    }
}
