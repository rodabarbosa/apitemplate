using ApiTemplate.Application.Contracts;
using FluentValidation;
using System;

namespace ApiTemplate.Application.Validators;

/// <summary>
/// This is a Weather Forecast validator.
/// </summary>
public class CreateWeatherForecastValidator : AbstractValidator<CreateWeatherForecastRequestContract>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateWeatherForecastValidator"/> class.
    /// </summary>
    public CreateWeatherForecastValidator()
    {
        RuleFor(x => x.Date)
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now.AddMinutes(1));

        RuleFor(x => x.TemperatureCelsius)
            .NotNull()
            .LessThanOrEqualTo(200)
            .GreaterThanOrEqualTo(-200);

        RuleFor(x => x.Summary)
            .Length(0, 250);
    }
}
