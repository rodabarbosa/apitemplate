using ApiTemplate.Application.Contracts;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTemplate.Application;

public static class WeatherForecastExtension
{
    public static GetWeatherForecastResponseContract ToContract(this WeatherForecast weather)
    {
        return new GetWeatherForecastResponseContract
        {
            Id = weather.Id,
            Date = weather.Date,
            TemperatureCelsius = weather.TemperatureCelsius,
            TemperatureFahrenheit = weather.TemperatureCelsius.ToFahrenheit(),
            Summary = weather.Summary
        };
    }

    public static GetWeatherForecastResponseContract ToContract(this WeatherForecast weather, IEnumerable<string> fields)
    {
        var hasInformedFields = fields.Any();
        if (!hasInformedFields)
            return weather.ToContract();

        return new GetWeatherForecastResponseContract
        {
            Id = HasField(fields, "id") ? weather.Id : null,
            Date = HasField(fields, "date") ? weather.Date : null,
            TemperatureCelsius = HasField(fields, "temperatureCelsius") ? weather.TemperatureCelsius : null,
            TemperatureFahrenheit = HasField(fields, "temperatureFahrenheit") ? weather.TemperatureCelsius.ToFahrenheit() : null,
            Summary = HasField(fields, "summary") ? weather.Summary : null
        };
    }

    private static bool HasField(IEnumerable<string> returningFields, string fieldName)
    {
        return returningFields.Any(x => x.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
    }
}
