using ApiTemplate.Application.Contracts;
using ApiTemplate.Shared.Extensions;
using FluentValidation;
using System;

namespace ApiTemplate.Application.Validators;

/// <summary>
/// Update weather forecast request validator.
/// </summary>
public class UpdateWeatherForecastValidator : AbstractValidator<UpdateWeatherForecastRequestContract>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateWeatherForecastValidator"/> class.
    /// </summary>
    public UpdateWeatherForecastValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual(Guid.Empty);

        RuleFor(x => x.Date)
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now);

        RuleFor(x => x.TemperatureCelsius)
            .NotNull()
            .LessThanOrEqualTo(200)
            .GreaterThanOrEqualTo(-200);

        RuleFor(x => x.TemperatureFahrenheit)
            .NotNull();

        RuleFor(x => x)
            .Custom((contract, context) =>
            {
                if (contract.TemperatureCelsius is null)
                    return;

                if (contract.TemperatureFahrenheit is null)
                    return;

                var fahrenheit = contract.TemperatureCelsius?.ToFahrenheit();
                if (fahrenheit != contract.TemperatureFahrenheit)
                    context.AddFailure(nameof(contract.TemperatureFahrenheit), "Temperature Fahrenheit and Celsius does not match.");
            });

        RuleFor(x => x.Summary)
            .Length(0, 250);
    }
}
