using System;
using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using ApiTemplate.WebApi.Controllers;
using Xunit;

namespace ApiTemplate.Tests.WebApiProjectTest.Controllers;

public class AuthenticationControllerTest : IDisposable
{
    private readonly AuthenticationController _controller;
    private readonly ApiTemplateContext _context;

    private const string AudienceIssuer = "teste";
    private const int Expires = 10000;

    public AuthenticationControllerTest()
    {
        _context = ContextUtil.GetContext();
        IUserRepository userRepository = new UserRepository(_context);
        ISigningConfiguration signingConfiguration = new SigningConfiguration();
        ITokenConfiguration tokenConfiguration = new TokenConfiguration { Audience = AudienceIssuer, Issuer = AudienceIssuer, Seconds = Expires };
        IAuthenticationService authenticationService = new AuthenticationService(signingConfiguration, tokenConfiguration, userRepository);
        _controller = new AuthenticationController(authenticationService);
    }

    [Theory]
    [InlineData("admin", "admin", false)]
    [InlineData("admin", "admin@123", true)]
    public async Task ShouldAuthenticate(string username, string password, bool authenticated)
    {
        var input = new AuthenticationModel
        {
            Username = username,
            Password = password
        };
        var result = await _controller.PostAsync(input);
        var data = result.Value;

        Assert.Equal(data?.Authenticated, authenticated);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
