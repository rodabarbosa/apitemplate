using System;

namespace ApiTemplate.Domain.Entities;

public sealed class WeatherForecast
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }

    public double TemperatureC { get; set; }

    public string Summary { get; set; }
}
