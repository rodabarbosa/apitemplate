using System;
using System.Linq;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using Xunit;

namespace ApiTemplate.Tests.DataProjectTest;

public class WeatherForecastRepositoryTest : IDisposable
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly ApiTemplateContext _context;

    public WeatherForecastRepositoryTest()
    {
        _context = ContextUtil.GetContext();
        _weatherForecastRepository = new WeatherForecastRepository(_context);
    }

    [Fact]
    public void GetAll_ShouldReturnAllWeatherForecasts()
    {
        var weatherForecasts = _weatherForecastRepository.Get();
        Assert.NotEmpty(weatherForecasts);
    }

    [Fact]
    public void GetByFilter_ShouldReturnWeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.FirstOrDefault();

        var result = _weatherForecastRepository.Get(x => x.TemperatureC == weatherForecast.TemperatureC).FirstOrDefault();

        Assert.NotNull(result);
    }

    [Fact]
    public void GetById_ShouldReturnWeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.FirstOrDefault();

        var result = _weatherForecastRepository.Get(x => x.Id == weatherForecast.Id).FirstOrDefault();

        Assert.NotNull(result);
    }

    [Fact]
    public void Adding_WeatherForecast()
    {
        var defaultId = Guid.Empty;
        var weather = new WeatherForecast
        {
            Id = defaultId,
            Date = DateTime.Today,
            TemperatureC = 10,
            Summary = "Test Summary"
        };

        _weatherForecastRepository.Add(weather);
        Assert.NotEqual(defaultId, weather.Id);
    }

    [Fact]
    public void Updating_WeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.FirstOrDefault();

        weatherForecast.TemperatureC = 20;

        _weatherForecastRepository.Update(weatherForecast);

        var result = _weatherForecastRepository.Get(x => x.TemperatureC == weatherForecast.TemperatureC).FirstOrDefault();

        Assert.NotNull(result);
    }

    [Fact]
    public void Deleting_WeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.FirstOrDefault();

        _weatherForecastRepository.Delete(weatherForecast);

        var result = _weatherForecastRepository.Get(x => x.Id == weatherForecast.Id).FirstOrDefault();

        Assert.Null(result);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}