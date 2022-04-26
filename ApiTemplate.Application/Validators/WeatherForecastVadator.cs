using System;
using ApiTemplate.Application.Models;
using FluentValidation;

namespace ApiTemplate.Application.Validators;

/// <summary>
/// This is a Weather Forecast validator.
/// </summary>
public class WeatherForecastValidator : AbstractValidator<WeatherForecastDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastValidator"/> class.
    /// </summary>
    public WeatherForecastValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now);

        RuleFor(x => x.TemperatureC)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(200);
    }
}
