using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Services;
using ApiTemplate.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
public class AuthenticationController : BaseController
{
    /// <summary>
    ///     Authenticate user
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request">Authentication data</param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticateResponseContract), StatusCodes.Status201Created)]
    public async Task<ActionResult<AuthenticateResponseContract>> PostAsync([FromServices] IAuthenticateService service, [FromBody] AuthenticateRequestContract request)
    {
        var response = await service.Authenticate(request);

        return Created("authenticate", response);
    }
}
