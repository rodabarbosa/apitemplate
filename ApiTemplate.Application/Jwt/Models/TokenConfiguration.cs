using ApiTemplate.Application.Jwt.Interfaces;

namespace ApiTemplate.Application.Jwt.Models;

/// <inheritdoc />
public sealed class TokenConfiguration : ITokenConfiguration
{
    /// <inheritdoc />
    public string Audience { get; set; }

    /// <inheritdoc />
    public string Issuer { get; set; }

    /// <inheritdoc />
    public int Seconds { get; set; }
}
