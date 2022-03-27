using ApiTemplate.Shared.Extensions;
using System;

namespace ApiTemplate.Application.Models;

public class WeatherForecastDto
{
    public Guid? Id { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => TemperatureC.ToFahrenheit();

    public string Summary { get; set; }
}