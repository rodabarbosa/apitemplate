using System;
using System.Collections.Generic;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Services;

namespace ApiTemplate.Application.Interfaces;

/// <summary>
/// The interface for the <see cref="WeatherForecastService"/> class.
/// </summary>
public interface IWeatherForecastService
{
    /// <summary>
    /// Gets a list of weather forecasts.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="temperatureCelsius"></param>
    /// <param name="temperatureFahrenheit"></param>
    /// <returns></returns>
    IEnumerable<WeatherForecastDto> GetWeatherForecasts(OperationParam<DateTime> date, OperationParam<double> temperatureCelsius, OperationParam<double> temperatureFahrenheit);

    /// <summary>
    /// Gets a  weather forecasts.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    WeatherForecastDto GetWeatherForecast(Guid id);

    /// <summary>
    /// Creates a  weather forecasts.
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    WeatherForecastDto AddWeatherForecast(WeatherForecastDto weatherForecast);

    /// <summary>
    /// Updates a  weather forecasts.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    WeatherForecastDto UpdateWeatherForecast(Guid id, WeatherForecastDto weatherForecast);

    /// <summary>
    /// Deletes a  weather forecasts.
    /// </summary>
    /// <param name="id"></param>
    void DeleteWeatherForecast(Guid id);
}
