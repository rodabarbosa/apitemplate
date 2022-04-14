namespace ApiTemplate.Application.Models;

/// <summary>
/// Container for authentication request
/// </summary>
public class AuthenticationModel
{
    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string Password { get; set; }
}
