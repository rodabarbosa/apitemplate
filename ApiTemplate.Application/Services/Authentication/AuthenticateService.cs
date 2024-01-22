using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Validators;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services.Authentication;

/// <inheritdoc />
public sealed class AuthenticateService : IAuthenticateService
{
    private const string DateFormat = "yyyy-MM-dd HH:mm:ss";
    private readonly ISigningConfiguration _signingConfiguration;
    private readonly ITokenConfiguration _tokenConfiguration;
    private readonly IUserRepository _userRepository;
    private readonly AuthenticationValidator _validator = new();

    /// <summary>
    /// Authentication service
    /// </summary>
    /// <param name="signingConfiguration"></param>
    /// <param name="tokenConfiguration"></param>
    /// <param name="userRepository"></param>
    public AuthenticateService(ISigningConfiguration signingConfiguration,
        ITokenConfiguration tokenConfiguration,
        IUserRepository userRepository)
    {
        _signingConfiguration = signingConfiguration;
        _tokenConfiguration = tokenConfiguration;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    async public Task<AuthenticateResponseContract> Authenticate(AuthenticateRequestContract request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        ValidationException.ThrowIf(!validationResult.IsValid, validationResult.Errors);

        var credentialsValidates = await _userRepository.IsUserValidAsync(request.Username!, request.Password!);

        BadRequestException.ThrowIf(!credentialsValidates, "Invalid credentials");

        var identity = CreateIdentity();

        return CreateResponse(identity, request.Username!);
    }

    static private ClaimsIdentity CreateIdentity()
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

    private string CreateToken(ClaimsIdentity identity,
        DateTime createdDate,
        DateTime expiresDate)
    {
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfiguration.Issuer,
            Audience = _tokenConfiguration.Audience,
            SigningCredentials = _signingConfiguration.SigningCredentials,
            Subject = identity,
            NotBefore = createdDate,
            Expires = expiresDate
        });
        return handler.WriteToken(securityToken);
    }

    private AuthenticateResponseContract CreateResponse(ClaimsIdentity identity, string username)
    {
        var createdDate = DateTime.Now;
        var expiresDate = createdDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

        var token = CreateToken(identity, createdDate, expiresDate);

        return new AuthenticateResponseContract
        {
            Created = createdDate.ToString(DateFormat),
            Expires = expiresDate.ToString(DateFormat),
            AccessToken = token,
            Username = username
        };
    }
}
