using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.WebApi.Controllers.Base;
using ApiTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> Get(string param = null)
    {
        var date = param.ExtractDateParam();
        var temperatureC = param.ExtractTemperatureCelsiusParam();
        var temperatureF = param.ExtractTemperatureFahrenheitParam();

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
    public Task<ActionResult<WeatherForecastDto>> Get(Guid id)
    {
        return Task.FromResult<ActionResult<WeatherForecastDto>>(_weatherForecastService.GetWeatherForecast(id));
    }

    /// <summary>
    ///   Create a new weather forecast
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<WeatherForecastDto>> Post([FromBody] WeatherForecastDto weatherForecast)
    {
        var result = _weatherForecastService.AddWeatherForecast(weatherForecast);

        Response.StatusCode = (int)HttpStatusCode.Created;

        return result;
    }

    /// <summary>
    ///  Update a weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<WeatherForecastDto>> Put(Guid id, [FromBody] WeatherForecastDto weatherForecast)
    {
        var result = _weatherForecastService.UpdateWeatherForecast(id, weatherForecast);

        Response.StatusCode = (int)HttpStatusCode.NoContent;

        return result;
    }

    /// <summary>
    ///  Delete a weather forecast
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(Guid id)
    {
        _weatherForecastService.DeleteWeatherForecast(id);

        return NoContent();
    }
}
