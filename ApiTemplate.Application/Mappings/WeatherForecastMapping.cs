using System;
using System.Collections.Generic;
using System.Linq;
using ApiTemplate.Application.Models;
using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Application.Mappings;

public static class WeatherForecastMapping
{
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

    public static IEnumerable<WeatherForecastDto> ToDtos(this IEnumerable<WeatherForecast> weatherForecasts)
    {
        if (weatherForecasts == null) return default;

        return weatherForecasts.Select(ToDto);
    }

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

    public static IEnumerable<WeatherForecast> ToEntities(this IEnumerable<WeatherForecastDto> weatherForecastDtos)
    {
        if (weatherForecastDtos == null) return default;

        return weatherForecastDtos.Select(ToEntity);
    }
}
