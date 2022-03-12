using ApiTemplate.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers.Base;

[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status403Forbidden)]
[Authorize("Bearer")]
public abstract class BaseApiAuthorizedController : BaseApiController
{
}