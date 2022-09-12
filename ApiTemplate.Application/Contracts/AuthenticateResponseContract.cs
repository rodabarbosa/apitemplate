namespace ApiTemplate.Application.Contracts;

/// <summary>
/// Authenticate Response
/// </summary>
public class AuthenticateResponseContract
{
    /// <summary>
    /// DateTime of authentication
    /// </summary>
    public string Created { get; set; } = string.Empty;

    /// <summary>
    /// DateTime of expiration
    /// </summary>
    public string Expires { get; set; } = string.Empty;

    /// <summary>
    /// Token
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Username authenticated
    /// </summary>
    public string Username { get; set; } = string.Empty;
}
