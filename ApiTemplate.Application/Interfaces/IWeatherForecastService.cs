using System;
using System.Collections.Generic;
using ApiTemplate.Application.Models;

namespace ApiTemplate.Application.Interfaces;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecastDto> GetWeatherForecasts(OperationParam<DateTime>? date, OperationParam<int>? temperatureC, OperationParam<int>? temparatureF);
    WeatherForecastDto GetWeatherForecast(Guid id);
    WeatherForecastDto AddWeatherForecast(WeatherForecastDto weatherForecast);
    WeatherForecastDto UpdateWeatherForecast(Guid id, WeatherForecastDto weatherForecast);
    void DeleteWeatherForecast(Guid id);
}