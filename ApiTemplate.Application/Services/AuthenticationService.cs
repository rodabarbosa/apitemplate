using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;

namespace ApiTemplate.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    public async Task<bool> Authenticate(string username, string password)
    {
        return username.Equals("admin") && password.Equals("admin@123");
    }
}
