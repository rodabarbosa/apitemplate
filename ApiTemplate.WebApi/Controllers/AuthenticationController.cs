using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiTemplate.WebApi.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// </summary>
    /// <param name="authenticationService"></param>
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    ///     Authenticate user
    /// </summary>
    /// <param name="input">Authentication data</param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<TokenModel>> PostAsync([FromBody] AuthenticationModel input)
    {
        var response = await _authenticationService.Authenticate(input.Username, input.Password);

        Response.StatusCode = (int)HttpStatusCode.Created;
        return response;
    }
}
