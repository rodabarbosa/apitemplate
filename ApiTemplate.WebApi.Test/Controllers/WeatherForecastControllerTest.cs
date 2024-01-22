using ApiTemplate.Application.Services.WeatherForecasts;
using ApiTemplate.Shared.Exceptions;

namespace ApiTemplate.WebApi.Test.Controllers;

public class WeatherForecastControllerTest : BaseControllerTest
{
    private readonly WeatherForecastController _controller = new();

    [Theory]
    [InlineData(null)]
    [InlineData("date=[Equal,2030-01-01 00:00:00]")]
    async public Task GetAll_ReturnsWeatherForecasts(string? param)
    {
        var context = ContextUtil.GetContext();
        var repository = new WeatherForecastRepository(context);
        var service = new GetAllWeatherForecastsService(repository);

        var act = () => _controller.GetAsync(service, param);

        await act.Should()
            .NotThrowAsync();
    }

    [Theory]
    [InlineData("10fd1392-3b4c-431a-b6dc-19cfba4ea269")]
    async public Task Get_ReturnsWeatherForecasts_Success(Guid id)
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new GetWeatherForecastService(reposity);

        var act = () => _controller.GetASync(service, id, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }

    [Theory]
    [InlineData("10fd1392-3b4c-431a-b6dc-19cfba4ea000")]
    async public Task Get_ReturnsWeatherForecasts_Fail(Guid id)
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new GetWeatherForecastService(reposity);

        var act = () => _controller.GetASync(service, id, CancellationToken.None);
        await act.Should()
            .ThrowAsync<NotFoundException>();
    }

    [Fact]
    async public Task Create_ReturnsWeatherForecasts()
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

        var act = () => _controller.PostAsync(service, request, CancellationToken.None);
        await act.Should()
            .NotThrowAsync();
    }

    [Fact]
    async public Task Update_ReturnsWeatherForecasts()
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

        var act = () => _controller.PutAsync(service, request.Id.Value, request, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }

    [Fact]
    async public Task Delete_ReturnsWeatherForecasts()
    {
        var context = ContextUtil.GetContext();
        var reposity = new WeatherForecastRepository(context);
        var service = new DeleteWeatherForecastService(reposity);
        var id = Guid.Parse("10fd1392-3b4c-431a-b6dc-19cfba4ea269");

        var act = () => _controller.DeleteAsync(service, id, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }
}
