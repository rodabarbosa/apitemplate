using System.Threading.Tasks;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using System;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Services;

public sealed class AuthenticationServiceTest : IDisposable
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ApiTemplateContext _context;

    private const string AudienceIssuer = "teste";
    private const int Expires = 10000;

    public AuthenticationServiceTest()
    {
        // Arrange
        ISigningConfiguration signingConfiguration = new SigningConfiguration();
        ITokenConfiguration tokenConfiguration = new TokenConfiguration { Audience = AudienceIssuer, Issuer = AudienceIssuer, Seconds = Expires };

        _context = ContextUtil.GetContext();
        IUserRepository userRepository = new UserRepository(_context);

        _authenticationService = new AuthenticationService(signingConfiguration, tokenConfiguration, userRepository);
    }

    [Theory]
    [InlineData("admin", "admin", false)]
    [InlineData("admin", "admin@123", true)]
    public async Task Should_Authenticate_User(string username, string password, bool expectedAssert)
    {
        // Act
        var result = await _authenticationService.Authenticate(username, password);

        // Assert
        if (expectedAssert)
            Assert.True(result.Authenticated);
        else
            Assert.False(result.Authenticated);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
