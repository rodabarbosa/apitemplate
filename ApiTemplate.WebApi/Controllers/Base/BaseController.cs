using ApiTemplate.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers.Base;

/// <summary>
/// Base controller for all controllers.
/// </summary>
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
}
