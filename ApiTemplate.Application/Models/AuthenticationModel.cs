namespace ApiTemplate.Application.Models;

/// <summary>
/// Container for authentication request
/// </summary>
public sealed class AuthenticationModel
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; init; }
}
