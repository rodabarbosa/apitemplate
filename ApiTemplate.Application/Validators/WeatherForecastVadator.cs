using ApiTemplate.Application.Models;
using FluentValidation;

namespace ApiTemplate.Application.Validators;

public class WeatherForecastValidator : AbstractValidator<WeatherForecastDto>
{
    public WeatherForecastValidator()
    {
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.TemperatureC).NotEmpty().LessThanOrEqualTo(100);
    }
}