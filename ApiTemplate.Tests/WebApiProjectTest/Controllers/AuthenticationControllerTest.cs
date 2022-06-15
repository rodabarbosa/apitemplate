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
    private readonly IAuthenticationService _authenticationService;
    private readonly AuthenticationController _controller;
    private readonly ISigningConfiguration _signingConfiguration;
    private readonly ITokenConfiguration _tokenConfiguration;
    private readonly IUserRepository _userRepository;
    private readonly ApiTemplateContext _context;

    private const string AudienceIssuer = "teste";
    private const int Expires = 10000;

    public AuthenticationControllerTest()
    {
        _context = ContextUtil.GetContext();
        _userRepository = new UserRepository(_context);
        _signingConfiguration = new SigningConfiguration();
        _tokenConfiguration = new TokenConfiguration {Audience = AudienceIssuer, Issuer = AudienceIssuer, Seconds = Expires};
        _authenticationService = new AuthenticationService(_signingConfiguration, _tokenConfiguration, _userRepository);
        _controller = new AuthenticationController(_authenticationService);
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
        var data = result?.Value;

        Assert.Equal(data.Authenticated, authenticated);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}