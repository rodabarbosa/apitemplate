using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<object> PostAsync([FromBody] AuthenticationModel input)
    {
        return await _authenticationService.Authenticate(input.Username, input.Password);
    }
}
