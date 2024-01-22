using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Extensions;
using ApiTemplate.Application.Models;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services.WeatherForecasts;

/// <inheritdoc />
public class GetAllWeatherForecastsService : IGetAllWeatherForecastsService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="weatherForecastRepository"></param>
    public GetAllWeatherForecastsService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GetWeatherForecastResponseContract>> GetAllWeatherForecastsAsync(string? param, CancellationToken cancellationToken)
    {
        var weathers = GetWeatherForecast(param);

        var returningFields = GetReturningFields(param);

        var result = PrepareWeatherAsResult(returningFields, weathers);

        return await result.ToListAsync(cancellationToken);
    }

    private static IQueryable<GetWeatherForecastResponseContract> PrepareWeatherAsResult(IEnumerable<string> returningFields, IQueryable<WeatherForecast> weathers)
    {
        var result = !returningFields.Any()
            ? weathers.Select(x => new GetWeatherForecastResponseContract
            {
                Id = x.Id,
                Date = x.Date,
                TemperatureCelsius = x.TemperatureCelsius,
                TemperatureFahrenheit = x.TemperatureCelsius.ToFahrenheit(),
                Summary = x.Summary
            })
            : weathers.Select(x => new GetWeatherForecastResponseContract
            {
                Id = HasField(returningFields, "id") ? x.Id : null,
                Date = HasField(returningFields, "date") ? x.Date : null,
                TemperatureCelsius = HasField(returningFields, "temperatureCelsius") ? x.TemperatureCelsius : null,
                TemperatureFahrenheit = HasField(returningFields, "temperatureFahrenheit") ? x.TemperatureCelsius.ToFahrenheit() : null,
                Summary = HasField(returningFields, "summary") ? x.Summary : null
            });
        return result;
    }

    private static bool HasField(IEnumerable<string> returningFields, string fieldName)
    {
        return returningFields.Any(x => x.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
    }

    private IQueryable<WeatherForecast> GetWeatherForecast(string? param)
    {
        var date = param?.ExtractDateParam();
        var temperatureCelsius = param?.ExtractTemperatureCelsiusParam();
        var temperatureFahrenheit = param?.ExtractTemperatureFahrenheitParam();

        var weathers = _weatherForecastRepository.Get();
        weathers = FilterByDate(weathers, date);
        weathers = FilterByTemperatureCelsius(weathers, temperatureCelsius);
        weathers = FilterByTemperatureFahrenheit(weathers, temperatureFahrenheit);
        return weathers;
    }

    private static IQueryable<WeatherForecast> FilterByDate(IQueryable<WeatherForecast> weathers, OperationParam<DateTime>? filter)
    {
        return filter?.Operation switch
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

    private static IQueryable<WeatherForecast> FilterByTemperatureCelsius(IQueryable<WeatherForecast> weathers, OperationParam<decimal>? filter)
    {
        return filter?.Operation switch
        {
            Operation.GreaterThan => weathers.Where(w => w.TemperatureCelsius > filter.Value),
            Operation.LessThan => weathers.Where(w => w.TemperatureCelsius < filter.Value),
            Operation.Equal => weathers.Where(w => w.TemperatureCelsius == filter.Value),
            Operation.NotEqual => weathers.Where(w => w.TemperatureCelsius != filter.Value),
            Operation.GreaterThanOrEqual => weathers.Where(w => w.TemperatureCelsius >= filter.Value),
            Operation.LessThanOrEqual => weathers.Where(w => w.TemperatureCelsius <= filter.Value),
            _ => weathers
        };
    }

    private static IQueryable<WeatherForecast> FilterByTemperatureFahrenheit(IQueryable<WeatherForecast> weathers, OperationParam<decimal>? filter)
    {
        return filter?.Operation switch
        {
            Operation.GreaterThan => weathers.Where(w => w.TemperatureCelsius.ToFahrenheit() > filter.Value),
            Operation.LessThan => weathers.Where(w => w.TemperatureCelsius.ToFahrenheit() < filter.Value),
            Operation.Equal => weathers.Where(w => w.TemperatureCelsius.ToFahrenheit() == filter.Value),
            Operation.NotEqual => weathers.Where(w => w.TemperatureCelsius.ToFahrenheit() != filter.Value),
            Operation.GreaterThanOrEqual => weathers.Where(w => w.TemperatureCelsius.ToFahrenheit() >= filter.Value),
            Operation.LessThanOrEqual => weathers.Where(w => w.TemperatureCelsius.ToFahrenheit() <= filter.Value),
            _ => weathers
        };
    }

    private IEnumerable<string> GetReturningFields(string? param)
    {
        if (string.IsNullOrEmpty(param) || !param.Contains("fields="))
            return Enumerable.Empty<string>();

        var values = param.Split('&');
        var fields = values
            .First(x => x.StartsWith("fields="));
        var aux = fields.Split("=");

        return aux[1]
            .TrimStart('[')
            .TrimEnd(']')
            .Split(',')
            .Select(x => x.Trim());
    }
}
