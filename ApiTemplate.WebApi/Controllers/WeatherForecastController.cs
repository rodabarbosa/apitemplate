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
    ///     For returning fiels use: fields=[field1,field2,field3]
    ///
    ///     Fields: id, date, temperatureCelsiues, temperatureFahrenheit, summary
    ///
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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetWeatherForecastResponseContract>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<GetWeatherForecastResponseContract>>> GetAsync([FromServices] IGetAllWeatherForecastsService service, string? param = null, CancellationToken cancellationToken = default)
    {
        var result = await service.GetAllWeatherForecastsAsync(param, cancellationToken);
        if (!result.Any())
            return NoContent();

        return Ok(result);
    }

    /// <summary>
    /// Get a specific weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetWeatherForecastResponseContract), StatusCodes.Status200OK)]
    [AllowAnonymous]
    public async Task<ActionResult<GetWeatherForecastResponseContract>> GetASync([FromServices] IGetWeatherForecastService service, Guid? id, CancellationToken cancellationToken)
    {
        var result = await service.GetWeatherForecastAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///   Create a new weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(GetWeatherForecastResponseContract), StatusCodes.Status201Created)]
    public async Task<ActionResult<GetWeatherForecastResponseContract>> PostAsync([FromServices] ICreateWeatherForecastService service,
        [FromBody] CreateWeatherForecastRequestContract request,
        CancellationToken cancellationToken)
    {
        var result = await service.CreateWeatherForecastAsync(request, cancellationToken);

        return Created($"/weatherforecast/{result.Id}", result);
    }

    /// <summary>
    ///  Update a weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> PutAsync([FromServices] IUpdateWeatherForecastService service, Guid id, [FromBody] UpdateWeatherForecastRequestContract request, CancellationToken cancellationToken)
    {
        _ = await service.UpdateWeatherForecastAsync(id, request, cancellationToken);

        return NoContent();
    }

    /// <summary>
    ///  Delete a weather forecast
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteAsync([FromServices] IDeleteWeatherForecastService service, Guid? id, CancellationToken cancellationToken)
    {
        await service.DeleteWeatherForecastAsync(id, cancellationToken);

        return NoContent();
    }
}
