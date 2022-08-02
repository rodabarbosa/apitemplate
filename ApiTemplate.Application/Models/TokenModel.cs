namespace ApiTemplate.Application.Models;

/// <summary>
/// Response container of authentication method
/// </summary>
public sealed class TokenModel
{
    /// <summary>
    /// Is authenticated
    /// </summary>
    public bool Authenticated { get; set; }

    /// <summary>
    /// DateTime of authentication
    /// </summary>
    public string Created { get; set; }

    /// <summary>
    /// DateTime of expiration
    /// </summary>
    public string Expires { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Username authenticated
    /// </summary>
    public string Username { get; set; }
}
