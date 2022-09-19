using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Validators;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Exceptions;
using System;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services.WeatherForecasts;

/// <inheritdoc />
public class CreateWeatherForecastService : ICreateWeatherForecastService
{
    private readonly CreateWeatherForecastValidator _validator = new();
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="weatherForecastRepository"></param>
    public CreateWeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <inheritdoc />
    public async Task<GetWeatherForecastResponseContract> CreateWeatherForecastAsync(CreateWeatherForecastRequestContract request)
    {
        Validate(request);

        var weather = await CreateWeatherForecast(request);

        return CreateResponse(weather);
    }

    private void Validate(CreateWeatherForecastRequestContract request)
    {
        var validationResult = _validator.Validate(request);
        ValidationException.ThrowIf(!validationResult.IsValid, validationResult.Errors);
    }

    private async Task<WeatherForecast> CreateWeatherForecast(CreateWeatherForecastRequestContract request)
    {
        var weather = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = request.Date ?? DateTime.Now,
            TemperatureCelsius = request.TemperatureCelsius ?? default,
            Summary = request.Summary
        };

        await _weatherForecastRepository.AddAsync(weather);

        return weather;
    }

    private static GetWeatherForecastResponseContract CreateResponse(WeatherForecast weather)
    {
        return new GetWeatherForecastResponseContract
        {
            Id = weather.Id,
            Date = weather.Date,
            TemperatureCelsius = weather.TemperatureCelsius,
            Summary = weather.Summary
        };
    }
}
