using ApiTemplate.Application.Contracts;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services.WeatherForecasts;

/// <inheritdoc />
public class GetWeatherForecastService : IGetWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="weatherForecastRepository"></param>
    public GetWeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <inheritdoc />
    public async Task<GetWeatherForecastResponseContract> GetWeatherForecastAsync(Guid? weatherForecastId, CancellationToken cancellationToken)
    {
        BadRequestException.ThrowIf(weatherForecastId is null, "Weather forecast identification is required.");

        var weather = await _weatherForecastRepository.GetAsync(weatherForecastId!.Value, cancellationToken);

        NotFoundException.ThrowIf(weather is null, $"No weather forecast found by id {weatherForecastId}");

        return new GetWeatherForecastResponseContract
        {
            Id = weather!.Id,
            Date = weather.Date,
            TemperatureCelsius = weather.TemperatureCelsius,
            Summary = weather.Summary
        };
    }
}
