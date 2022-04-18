using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.Application.Services;

/// <inheritdoc />
public class AuthenticationService : IAuthenticationService
{
    private readonly ISigningConfiguration _signingConfiguration;
    private readonly ITokenConfiguration _tokenConfiguration;
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

    private const string DefaultUsername = "admin";
    private const string DefaultPassword = "admin@123";

    /// <summary>
    /// Authentication service
    /// </summary>
    /// <param name="signingConfiguration"></param>
    /// <param name="tokenConfiguration"></param>
    public AuthenticationService(ISigningConfiguration signingConfiguration,
        ITokenConfiguration tokenConfiguration)
    {
        _signingConfiguration = signingConfiguration;
        _tokenConfiguration = tokenConfiguration;
    }

    private static bool TempAuthentication(string username, string password)
    {
        return username == DefaultUsername && password == DefaultPassword;
    }

    /// <inheritdoc />
    public async Task<TokenModel> Authenticate(string username, string password)
    {
        bool credentialsValidates = TempAuthentication(username, password);
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
            Created = createDate.ToString(DateFormat),
            Expires = expireDate.ToString(DateFormat),
            AccessToken = token,
            Username = username
        };
    }
}
