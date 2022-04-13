using ApiTemplate.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers.Base;

[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
[ApiController]
[Route("[controller]")]
public abstract class BaseApiController : ControllerBase
{
}
