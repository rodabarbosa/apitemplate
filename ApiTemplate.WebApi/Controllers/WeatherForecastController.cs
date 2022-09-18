using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Services;
using ApiTemplate.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers;

/// <summary>
/// WeatherForecast Controller
/// </summary>
public class WeatherForecastController : BaseAuthController
{
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
    /// <param name="service"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetWeatherForecastResponseContract>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetWeatherForecastResponseContract>>> GetAsync([FromServices] IGetAllWeatherForecastsService service, string? param = null)
    {
        var result = await service.GetAllWeatherForecastsAsync(param);
        if (!result.Any())
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Get a specific weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetWeatherForecastResponseContract), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<GetWeatherForecastResponseContract>> GetASync([FromServices] IGetWeatherForecastService service, Guid? id)
    {
        return Ok(await service.GetWeatherForecastAsync(id));
    }

    /// <summary>
    ///   Create a new weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(GetWeatherForecastResponseContract), StatusCodes.Status201Created)]
    public async Task<ActionResult<GetWeatherForecastResponseContract>> PostAsync([FromServices] ICreateWeatherForecastService service,
        [FromBody] CreateWeatherForecastRequestContract request)
    {
        var result = await service.CreateWeatherForecastAsync(request);

        return Created("weatherforecast", result);
    }

    /// <summary>
    ///  Update a weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> PutAsync([FromServices] IUpdateWeatherForecastService service, Guid id, [FromBody] UpdateWeatherForecastRequestContract request)
    {
        _ = await service.UpdateWeatherForecastAsync(id, request);

        return NoContent();
    }

    /// <summary>
    ///  Delete a weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAsync([FromServices] IDeleteWeatherForecastService service, Guid? id)
    {
        await service.DeleteWeatherForecastAsync(id);

        return NoContent();
    }
}
