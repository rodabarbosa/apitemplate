using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Application.Services.Authentication;
using ApiTemplate.Application.Test.Utils;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Shared.Exceptions;

namespace ApiTemplate.Application.Test.Services;

public sealed class AuthenticationServiceTest : IDisposable
{
    private const string AudienceIssuer = "teste";
    private const int Expires = 10000;
    private readonly IAuthenticateService _authenticateService;
    private readonly ApiTemplateContext _context;

    public AuthenticationServiceTest()
    {
        // Arrange
        ISigningConfiguration signingConfiguration = new SigningConfiguration();
        ITokenConfiguration tokenConfiguration = new TokenConfiguration { Audience = AudienceIssuer, Issuer = AudienceIssuer, Seconds = Expires };

        _context = ContextUtil.GetContext();
        IUserRepository userRepository = new UserRepository(_context);

        _authenticateService = new AuthenticateService(signingConfiguration, tokenConfiguration, userRepository);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Theory]
    [InlineData("admin", "admin", false)]
    [InlineData("admin", "admin@123", true)]
    public async Task Should_Authenticate_User(string username, string password, bool expectedAssert)
    {
        // Act
        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        // Assert
        if (expectedAssert)
        {
            var result = await _authenticateService.Authenticate(request);
            Assert.NotNull(result);
            return;
        }

        await Assert.ThrowsAsync<BadRequestException>(() => _authenticateService.Authenticate(request));
    }
}
