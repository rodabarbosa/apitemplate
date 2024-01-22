using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Services.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services;

/// <summary>
/// This is the interface for the <see cref="AuthenticateService"/> class.
/// </summary>
public interface IAuthenticateService
{
    /// <summary>
    /// This method is used to authenticate the user.
    /// </summary>
    Task<AuthenticateResponseContract> Authenticate(AuthenticateRequestContract request, CancellationToken cancellationToken);
}
