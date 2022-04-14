using System;
using ApiTemplate.Shared.Extensions;

namespace ApiTemplate.Application.Models;

/// <summary>
/// WeatherForecast model
/// </summary>
public class WeatherForecastDto
{
    /// <summary>
    /// Gets or sets weatherforecast identification
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast date
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast temperature in Celsius
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast temperature in Fahrenheit
    /// </summary>
    public int TemperatureF => TemperatureC.ToFahrenheit();

    /// <summary>
    /// Gets or sets weatherforecast summary
    /// </summary>
    public string Summary { get; set; }
}
