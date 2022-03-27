using System.Threading.Tasks;

namespace ApiTemplate.Application.Interfaces;

public interface IAuthenticationService
{
    Task<bool> Authenticate(string username, string password);

    Task<bool> Logout();
}