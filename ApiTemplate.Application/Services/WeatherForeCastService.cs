using System;
using System.Collections.Generic;
using System.Linq;
using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Mappings;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Validators;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Shared.Exceptions;
using ApiTemplate.Shared.Extensions;

namespace ApiTemplate.Application.Services;

/// <inheritdoc />
public sealed class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private const double Tolerance = 1e-6;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastService"/> class.
    /// </summary>
    /// <param name="weatherForecastRepository"></param>
    public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <inheritdoc />
    public IEnumerable<WeatherForecastDto> GetWeatherForecasts(OperationParam<DateTime> date, OperationParam<double> temperatureCelsius, OperationParam<double> temperatureFahrenheit)
    {
        var weathers = _weatherForecastRepository.Get();
        weathers = FilterByDate(weathers, date);
        weathers = FilterByTemperatureCelsius(weathers, temperatureCelsius);
        weathers = FilterByTemperatureFahrenheit(weathers, temperatureFahrenheit);

        return weathers.Select(x => x.ToDto());
    }

    /// <inheritdoc />
    public WeatherForecastDto GetWeatherForecast(Guid id)
    {
        var weather = _weatherForecastRepository.Get(id);
        return weather?.ToDto();
    }

    /// <inheritdoc />
    public WeatherForecastDto AddWeatherForecast(WeatherForecastDto weatherForecast)
    {
        Validate(weatherForecast);

        var weather = weatherForecast.ToEntity();
        _weatherForecastRepository.Add(weather);

        return weather.ToDto();
    }

    /// <inheritdoc />
    public WeatherForecastDto UpdateWeatherForecast(Guid id, WeatherForecastDto weatherForecast)
    {
        var weather = _weatherForecastRepository.Get(id);
        NotFoundException.ThrowIf(weather is null, $"WeatherForecast with id {id} not found");

        Validate(weatherForecast);

        weather!.Date = weatherForecast.Date;
        weather.Summary = weatherForecast.Summary;
        weather.TemperatureC = weatherForecast.TemperatureC;

        _weatherForecastRepository.Update(weather);
        return weather.ToDto();
    }

    private static void Validate(WeatherForecastDto weatherForecast)
    {
        var validator = new WeatherForecastValidator();
        var validationResult = validator.Validate(weatherForecast);
        ValidationException.ThrowIf(!validationResult.IsValid, validationResult.Errors);
    }

    /// <inheritdoc />
    public void DeleteWeatherForecast(Guid id)
    {
        var weather = _weatherForecastRepository.Get(id);
        NotFoundException.ThrowIf(weather is null, $"WeatherForecast with id {id} not found");

        _weatherForecastRepository.Delete(weather!.Id);
    }

    private static IQueryable<WeatherForecast> FilterByDate(IQueryable<WeatherForecast> weathers, OperationParam<DateTime> filter)
    {
        if (filter == null)
            return weathers;

        return filter.Operation switch
        {
            Operation.GreaterThan => weathers.Where(w => w.Date > filter.Value),
            Operation.LessThan => weathers.Where(w => w.Date < filter.Value),
            Operation.Equal => weathers.Where(w => w.Date == filter.Value),
            Operation.NotEqual => weathers.Where(w => w.Date != filter.Value),
            Operation.GreaterThanOrEqual => weathers.Where(w => w.Date >= filter.Value),
            Operation.LessThanOrEqual => weathers.Where(w => w.Date <= filter.Value),
            _ => weathers
        };
    }

    private static IQueryable<WeatherForecast> FilterByTemperatureCelsius(IQueryable<WeatherForecast> weathers, OperationParam<double> filter)
    {
        if (filter == null)
            return weathers;

        return filter.Operation switch
        {
            Operation.GreaterThan => weathers.Where(w => w.TemperatureC > filter.Value),
            Operation.LessThan => weathers.Where(w => w.TemperatureC < filter.Value),
            Operation.Equal => weathers.Where(w => Math.Abs(w.TemperatureC - filter.Value) < Tolerance),
            Operation.NotEqual => weathers.Where(w => Math.Abs(w.TemperatureC - filter.Value) > Tolerance),
            Operation.GreaterThanOrEqual => weathers.Where(w => w.TemperatureC >= filter.Value),
            Operation.LessThanOrEqual => weathers.Where(w => w.TemperatureC <= filter.Value),
            _ => weathers
        };
    }

    private static IQueryable<WeatherForecast> FilterByTemperatureFahrenheit(IQueryable<WeatherForecast> weathers, OperationParam<double> filter)
    {
        if (filter == null)
            return weathers;

        return filter.Operation switch
        {
            Operation.GreaterThan => weathers.Where(w => w.TemperatureC.ToFahrenheit() > filter.Value),
            Operation.LessThan => weathers.Where(w => w.TemperatureC.ToFahrenheit() < filter.Value),
            Operation.Equal => weathers.Where(w => Math.Abs(w.TemperatureC.ToFahrenheit() - filter.Value) < Tolerance),
            Operation.NotEqual => weathers.Where(w => Math.Abs(w.TemperatureC.ToFahrenheit() - filter.Value) > Tolerance),
            Operation.GreaterThanOrEqual => weathers.Where(w => w.TemperatureC.ToFahrenheit() >= filter.Value),
            Operation.LessThanOrEqual => weathers.Where(w => w.TemperatureC.ToFahrenheit() <= filter.Value),
            _ => weathers
        };
    }
}
