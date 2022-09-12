using ApiTemplate.Application.Jwt.Interfaces;

namespace ApiTemplate.Application.Jwt.Models;

/// <inheritdoc />
public sealed class TokenConfiguration : ITokenConfiguration
{
    /// <inheritdoc />
    public string Audience { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Issuer { get; set; } = string.Empty;

    /// <inheritdoc />
    public int Seconds { get; set; } = 0;
}
