using System;

namespace ApiTemplate.Application.Contracts;

/// <summary>
/// Weather forecast create request contract
/// </summary>
public class CreateWeatherForecastRequestContract
{
    /// <summary>
    /// Gets or sets weatherforecast date
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast temperature in Celsius
    /// </summary>
    public decimal? TemperatureCelsius { get; set; }

    /// <summary>
    /// Gets or sets weatherforecast summary
    /// </summary>
    public string? Summary { get; set; }
}
