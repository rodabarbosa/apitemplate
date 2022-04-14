using System.Threading.Tasks;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Services;

namespace ApiTemplate.Application.Interfaces;

/// <summary>
/// This is the interface for the <see cref="AuthenticationService"/> class.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// This method is used to authenticate the user.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<TokenModel> Authenticate(string username, string password);
}
