using System.IdentityModel.Tokens.Jwt;
using ApiTemplate.Application.Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.Application.Jwt.Interfaces;

/// <summary>
/// Interface for <see cref="SigningConfiguration"/> class.
/// </summary>
public interface ISigningConfiguration
{
    /// <summary>
    /// Gets the <see cref="SecurityKey"/> that is used to sign the <see cref="JwtSecurityToken"/>.
    /// </summary>
    SecurityKey Key { get; }

    /// <summary>
    /// Gets the <see cref="SigningCredentials"/> that is used to sign the <see cref="JwtSecurityToken"/>.
    /// </summary>
    SigningCredentials SigningCredentials { get; }
}
