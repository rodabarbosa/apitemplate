using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Models;
using ApiTemplate.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.Application.Services;

/// <inheritdoc />
public sealed class AuthenticationService : IAuthenticationService
{
    private readonly ISigningConfiguration _signingConfiguration;
    private readonly ITokenConfiguration _tokenConfiguration;
    private readonly IUserRepository _userRepository;
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// Authentication service
    /// </summary>
    /// <param name="signingConfiguration"></param>
    /// <param name="tokenConfiguration"></param>
    /// <param name="userRepository"></param>
    public AuthenticationService(ISigningConfiguration signingConfiguration,
        ITokenConfiguration tokenConfiguration,
        IUserRepository userRepository)
    {
        _signingConfiguration = signingConfiguration;
        _tokenConfiguration = tokenConfiguration;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<TokenModel> Authenticate(string username, string password)
    {
        bool credentialsValidates = await _userRepository.IsUserValidAsync(username, password);
        if (!credentialsValidates)
            return new TokenModel();

        ClaimsIdentity identity = CreateIdentity();

        DateTime createdDate = DateTime.Now;
        DateTime expiresDate = createdDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

        var token = CreateToken(identity, _signingConfiguration, _tokenConfiguration, createdDate, expiresDate);

        return new TokenModel
        {
            Authenticated = true,
            Created = createdDate.ToString(DateFormat),
            Expires = expiresDate.ToString(DateFormat),
            AccessToken = token,
            Username = username
        };
    }

    private static ClaimsIdentity CreateIdentity()
    {
        return new ClaimsIdentity(
            new GenericIdentity(Guid.NewGuid().ToString("N"), "Login"),
            new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, Guid.NewGuid().ToString("N"))
            }
        );
    }

    private static string CreateToken(ClaimsIdentity identity, ISigningConfiguration signingConfiguration, ITokenConfiguration tokenConfiguration, DateTime createdDate, DateTime expiresDate)
    {
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = tokenConfiguration.Issuer,
            Audience = tokenConfiguration.Audience,
            SigningCredentials = signingConfiguration.SigningCredentials,
            Subject = identity,
            NotBefore = createdDate,
            Expires = expiresDate
        });
        return handler.WriteToken(securityToken);
    }
}
