using System;

namespace ApiTemplate.Application.Contracts;

/// <summary>
/// WeatherForecast model
/// </summary>
public class GetWeatherForecastResponseContract
{
    /// <summary>
    /// Gets or sets weatherforecast identification
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast date
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast temperature in Celsius
    /// </summary>
    public decimal? TemperatureCelsius { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast temperature in Fahrenheit
    /// </summary>
    public decimal? TemperatureFahrenheit { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast summary
    /// </summary>
    public string? Summary { get; set; }
}
