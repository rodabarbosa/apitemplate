using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.WebApi.Controllers.Base;
using ApiTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers;

/// <summary>
/// WeatherForecast Controller
/// </summary>
public class WeatherForecastController : BaseAuthController
{
    private readonly IWeatherForecastService _weatherForecastService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
    /// </summary>
    /// <param name="weatherForecastService"></param>
    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    /// <summary>
    ///     List all weather forecasts
    /// </summary>
    /// <remarks>
    ///     For query use: field=[operationType,value]
    ///
    ///     Example: date=[GreaterThan,2019-01-01]
    ///
    ///     OperationType:
    ///     Equal,
    ///     NotEqual,
    ///     GreaterThan,
    ///     GreaterThanOrEqual,
    ///     LessThan,
    ///     LessThanOrEqual
    /// </remarks>
    /// <param name="param"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    [AllowAnonymous]
#pragma warning disable CS1998
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> Get(string param)
#pragma warning restore CS1998
    {
        OperationParam<DateTime>? date = param.ExtractDateParam();
        OperationParam<double>? temperatureC = param.ExtractTemperatureCelsiusParam();
        OperationParam<double>? temperatureF = param.ExtractTemperatureFahrenheitParam();

        var result = _weatherForecastService.GetWeatherForecasts(date, temperatureC, temperatureF);
        return Ok(result);
    }

    /// <summary>
    /// Get a specific weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public Task<ActionResult<WeatherForecastDto>> Get(Guid id) => Task.FromResult<ActionResult<WeatherForecastDto>>(_weatherForecastService.GetWeatherForecast(id));

    /// <summary>
    ///   Create a new weather forecast
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    public Task<ActionResult<WeatherForecastDto>> Post([FromBody] WeatherForecastDto weatherForecast) => Task.FromResult<ActionResult<WeatherForecastDto>>(_weatherForecastService.AddWeatherForecast(weatherForecast));

    /// <summary>
    ///  Update a weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    public Task<ActionResult<WeatherForecastDto>> Put(Guid id, [FromBody] WeatherForecastDto weatherForecast) => Task.FromResult<ActionResult<WeatherForecastDto>>(_weatherForecastService.UpdateWeatherForecast(id, weatherForecast));

    /// <summary>
    ///  Delete a weather forecast
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<ActionResult> Delete(Guid id)
    {
        _weatherForecastService.DeleteWeatherForecast(id);
        return Task.FromResult<ActionResult>(Ok());
    }
}
