using ApiTemplate.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers.Base;

[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
[Authorize("Bearer")]
public abstract class BaseAuthController : BaseController
{
}
