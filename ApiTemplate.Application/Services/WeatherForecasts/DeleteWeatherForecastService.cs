using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services.WeatherForecasts;

/// <inheritdoc />
public class DeleteWeatherForecastService : IDeleteWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="weatherForecastRepository"></param>
    public DeleteWeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <inheritdoc />
    public Task DeleteWeatherForecastAsync(Guid? weatherForecastId, CancellationToken cancellationToken)
    {
        BadRequestException.ThrowIf(weatherForecastId is null, "Weather forecast identification is required.");

        return _weatherForecastRepository.DeleteAsync(weatherForecastId!.Value, cancellationToken);
    }
}
