using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Infra.Data;
using Microsoft.AspNetCore.Identity;

namespace ApiTemplate.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticationService(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<bool> Authenticate(string username, string password)
    {
        return username.Equals("admin") && password.Equals("admin@123");
    }

    public async Task<bool> Logout()
    {
        await _signInManager.SignOutAsync();
        return true;
    }
}
