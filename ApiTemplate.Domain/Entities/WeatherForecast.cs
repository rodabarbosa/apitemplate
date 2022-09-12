using System;

namespace ApiTemplate.Domain.Entities;

public sealed class WeatherForecast
{
    public WeatherForecast(Guid id, DateTime date, decimal temperatureCelsius, string? summary) : this(id, date, temperatureCelsius)
    {
        Summary = summary;
    }

    public WeatherForecast(Guid id, DateTime date, decimal temperatureCelsius) : this(id, date)
    {
        TemperatureCelsius = temperatureCelsius;
    }

    public WeatherForecast(Guid id, DateTime date)
    {
        Id = id == Guid.Empty ? Guid.NewGuid() : id;
        Date = date;
    }

    public WeatherForecast(Guid id) : this(id, DateTime.Now)
    {
    }

    public WeatherForecast()
    {
        Id = Guid.NewGuid();
        Date = DateTime.Now;
    }

    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
}
