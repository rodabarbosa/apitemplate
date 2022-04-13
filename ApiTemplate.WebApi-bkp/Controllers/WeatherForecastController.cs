using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.WebApi.Controllers.Base;
using ApiTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace ApiTemplate.WebApi.Controllers;

public class WeatherForecastController : BaseApiController
{
    private readonly ILogger _logger;
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(ILogger logger,
        IWeatherForecastService weatherForecastService)
    {
        _logger = logger;
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
    public IEnumerable<WeatherForecastDto> Get(string param)
    {
        _logger.Information("Get all weather forecasts. Params: {@param}", param);
        OperationParam<DateTime>? date = param.ExtractDateParam();
        OperationParam<int>? temperatureC = param.ExtractTemperatureCParam();
        OperationParam<int>? temperatureF = param.ExtractTemperatureFParam();

        return _weatherForecastService.GetWeatherForecasts(date, temperatureC, temperatureF);
    }

    /// <summary>
    /// Get a specific weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public WeatherForecastDto Get(Guid id) => _weatherForecastService.GetWeatherForecast(id);

    /// <summary>
    ///   Create a new weather forecast
    /// </summary>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [Authorize]
    public WeatherForecastDto Post([FromBody] WeatherForecastDto weatherForecast) => _weatherForecastService.AddWeatherForecast(weatherForecast);

    /// <summary>
    ///  Update a weather forecast
    /// </summary>
    /// <param name="id"></param>
    /// <param name="weatherForecast"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [Authorize]
    public WeatherForecastDto Put(Guid id, [FromBody] WeatherForecastDto weatherForecast) => _weatherForecastService.UpdateWeatherForecast(id, weatherForecast);

    /// <summary>
    ///  Delete a weather forecast
    /// </summary>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public void Delete(Guid id) => _weatherForecastService.DeleteWeatherForecast(id);
}
