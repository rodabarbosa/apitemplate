namespace ApiTemplate.Application.Contracts;

/// <summary>
/// Container for authentication request
/// </summary>
public sealed class AuthenticateRequestContract
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string? Password { get; init; }
}
