using System;
using System.Collections.Generic;
using System.Linq;
using ApiTemplate.Application.Models;
using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Application.Mappings;

/// <summary>
/// This class maps the domain entity to the application model.
/// </summary>
public static class WeatherForecastMapping
{
    /// <summary>
    /// Maps the domain entity to the application model.
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    public static WeatherForecastDto ToDto(this WeatherForecast weatherForecast)
    {
        if (weatherForecast == null) return default;

        return new WeatherForecastDto
        {
            Id = weatherForecast.Id,
            Date = weatherForecast.Date,
            TemperatureC = weatherForecast.TemperatureC,
            Summary = weatherForecast.Summary
        };
    }

    /// <summary>
    /// Maps the domain entity to the application model.
    /// </summary>
    /// <param name="weatherForecasts"></param>
    /// <returns></returns>
    public static IEnumerable<WeatherForecastDto> ToDtos(this IEnumerable<WeatherForecast> weatherForecasts)
    {
        if (weatherForecasts == null) return default;

        return weatherForecasts.Select(ToDto);
    }

    /// <summary>
    /// Maps the application model to the domain entity.
    /// </summary>
    /// <param name="weatherForecastDto"></param>
    /// <returns></returns>
    public static WeatherForecast ToEntity(this WeatherForecastDto weatherForecastDto)
    {
        if (weatherForecastDto == null) return default;

        return new WeatherForecast
        {
            Id = weatherForecastDto.Id ?? Guid.Empty,
            Date = weatherForecastDto.Date,
            TemperatureC = weatherForecastDto.TemperatureC,
            Summary = weatherForecastDto.Summary
        };
    }

    /// <summary>
    /// Maps the application model to the domain entity.
    /// </summary>
    /// <param name="weatherForecastDtos"></param>
    /// <returns></returns>
    public static IEnumerable<WeatherForecast> ToEntities(this IEnumerable<WeatherForecastDto> weatherForecastDtos)
    {
        return weatherForecastDtos?.Select(ToEntity);
    }
}
