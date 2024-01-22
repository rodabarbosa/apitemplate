using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Validators;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services.WeatherForecasts;

/// <inheritdoc />
public class UpdateWeatherForecastService : IUpdateWeatherForecastService
{
    private readonly UpdateWeatherForecastValidator _validator = new();
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="weatherForecastRepository"></param>
    public UpdateWeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <inheritdoc />
    public async Task<GetWeatherForecastResponseContract> UpdateWeatherForecastAsync(Guid? weatherForecastId, UpdateWeatherForecastRequestContract request, CancellationToken cancellationToken)
    {
        BadRequestException.ThrowIf(weatherForecastId != request.Id, "Weather forecast identification mismatch.");

        Validate(request);

        var weather = await GetDatabaseWeatherForecast(request, cancellationToken);

        await UpdateWeatherForecastAsync(weather, cancellationToken);

        return CreateRespoonse(weather);
    }

    private void Validate(UpdateWeatherForecastRequestContract request)
    {
        var validationResult = _validator.Validate(request);
        ValidationException.ThrowIf(!validationResult.IsValid, validationResult.Errors);
    }

    private async Task<WeatherForecast> GetDatabaseWeatherForecast(UpdateWeatherForecastRequestContract request, CancellationToken cancellationToken)
    {
        var databaseWeatherForecast = await _weatherForecastRepository.GetAsync(request.Id!.Value, cancellationToken);

        NotFoundException.ThrowIf(databaseWeatherForecast is null, "Weather forecast not found.");

        databaseWeatherForecast!.Date = request.Date ?? DateTime.Now;
        databaseWeatherForecast.Summary = request.Summary ?? databaseWeatherForecast.Summary;
        databaseWeatherForecast.TemperatureCelsius = request.TemperatureCelsius ?? databaseWeatherForecast.TemperatureCelsius;

        return databaseWeatherForecast;
    }

    private Task UpdateWeatherForecastAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken)
    {
        return _weatherForecastRepository.UpdateAsync(weatherForecast, cancellationToken);
    }

    private static GetWeatherForecastResponseContract CreateRespoonse(WeatherForecast weatherForecast)
    {
        return new GetWeatherForecastResponseContract
        {
            Id = weatherForecast.Id,
            Date = weatherForecast.Date,
            Summary = weatherForecast.Summary,
            TemperatureCelsius = weatherForecast.TemperatureCelsius
        };
    }
}
