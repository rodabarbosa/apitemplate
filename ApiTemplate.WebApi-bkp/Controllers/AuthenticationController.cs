using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.Shared.Models;
using ApiTemplate.WebApi.Controllers.Base;
using ApiTemplate.WebApi.Jwt.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.WebApi.Controllers;

/// <summary>
///     Authentication resource
/// </summary>
[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ISigningConfiguration _signingConfiguration;
    private readonly ITokenConfiguration _tokenConfiguration;

    /// <summary>
    /// </summary>
    /// <param name="authenticationService"></param>
    /// <param name="signingConfiguration"></param>
    /// <param name="tokenConfiguration"></param>
    public AuthenticationController(IAuthenticationService authenticationService,
        ISigningConfiguration signingConfiguration,
        ITokenConfiguration tokenConfiguration)
    {
        _authenticationService = authenticationService;
        _signingConfiguration = signingConfiguration;
        _tokenConfiguration = tokenConfiguration;
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
        bool credentialsValidates = await _authenticationService.Authenticate(input.Username, input.Password);
        if (!credentialsValidates)
            return new TokenModel();

        ClaimsIdentity identity = new ClaimsIdentity(
            new GenericIdentity(Guid.NewGuid().ToString("N"), "Login"),
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, Guid.NewGuid().ToString("N"))
            }
        );

        DateTime createDate = DateTime.Now;
        DateTime expireDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfiguration.Issuer,
            Audience = _tokenConfiguration.Audience,
            SigningCredentials = _signingConfiguration.SigningCredentials,
            Subject = identity,
            NotBefore = createDate,
            Expires = expireDate
        });
        var token = handler.WriteToken(securityToken);

        return new TokenModel
        {
            Authenticated = true,
            Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            Expires = expireDate.ToString("yyyy-MM-dd HH:mm:ss"),
            AccessToken = token,
            Username = input.Username
        };
    }
}
