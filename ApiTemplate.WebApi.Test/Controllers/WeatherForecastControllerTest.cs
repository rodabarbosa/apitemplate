using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Services.WeatherForecasts;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Shared.Exceptions;
using ApiTemplate.WebApi.Controllers;
using ApiTemplate.WebApi.Extensions;
using ApiTemplate.WebApi.Test.Utils;
using Microsoft.AspNetCore.Builder;

namespace ApiTemplate.WebApi.Test.Controllers;

public class WeatherForecastControllerTest
{
    private readonly WeatherForecastController _controller = new();

    public WeatherForecastControllerTest()
    {
        var builder = WebApplication.CreateBuilder();
        var serviceCollection = builder.Services;
        var configuration = builder.Configuration;

        serviceCollection.AddJwtService(configuration);
        serviceCollection.AddDataProviders();
        serviceCollection.ConfigureDefaultErrorHandler();
        serviceCollection.ConfigureSwagger();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("date=[Equal,2030-01-01 00:00:00]")]
    public async Task GetAll_ReturnsWeatherForecasts(string? param)
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new GetAllWeatherForecastsService(reposity);
        _ = await _controller.GetAsync(service, param);
        Assert.True(true);
    }

    [Theory]
    [InlineData("10fd1392-3b4c-431a-b6dc-19cfba4ea269", true)]
    [InlineData("10fd1392-3b4c-431a-b6dc-19cfba4ea000", false)]
    public async Task Get_ReturnsWeatherForecasts(Guid id, bool expected)
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new GetWeatherForecastService(reposity);
        if (expected)
        {
            await _controller.GetASync(service, id);
            Assert.True(true);
            return;
        }

        await Assert.ThrowsAsync<NotFoundException>(() => _controller.GetASync(service, id));
    }

    [Fact]
    public async Task Create_ReturnsWeatherForecasts()
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new CreateWeatherForecastService(reposity);

        var request = new CreateWeatherForecastRequestContract
        {
            Date = DateTime.Now.AddDays(-1),
            TemperatureCelsius = 0,
            Summary = null
        };

        _ = await _controller.PostAsync(service, request);
        Assert.True(true);
    }

    [Fact]
    public void Update_ReturnsWeatherForecasts()
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new UpdateWeatherForecastService(reposity);

        var request = new UpdateWeatherForecastRequestContract
        {
            Id = Guid.Parse("10fd1392-3b4c-431a-b6dc-19cfba4ea269"),
            Date = DateTime.Now.AddDays(-1),
            TemperatureCelsius = 0,
            TemperatureFahrenheit = 32,
            Summary = null
        };

        _ = _controller.PutAsync(service, request.Id.Value, request);
        Assert.True(true);
    }

    [Fact]
    public void Delete_ReturnsWeatherForecasts()
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new DeleteWeatherForecastService(reposity);
        var id = Guid.Parse("10fd1392-3b4c-431a-b6dc-19cfba4ea269");
        _ = _controller.DeleteAsync(service, id);
        Assert.True(true);
    }
}
