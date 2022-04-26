using System.Threading.Tasks;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.Tests.Utils;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest;

public class AuthenticationServiceTest
{
    private readonly SigningConfiguration _signingConfiguration;
    private readonly TokenConfiguration _tokenConfiguration;
    private readonly AuthenticationService _authenticationService;
    private readonly UserRepository _userRepository;
    private readonly ApiTemplateContext _context;

    private const string AudienceIssuer = "teste";
    private const int Expires = 10000;

    public AuthenticationServiceTest()
    {
        // Arrange
        _signingConfiguration = new SigningConfiguration();
        _tokenConfiguration = new TokenConfiguration {Audience = AudienceIssuer, Issuer = AudienceIssuer, Seconds = Expires};

        _context = ContextUtil.GetContext();
        _userRepository = new UserRepository(_context);

        _authenticationService = new AuthenticationService(_signingConfiguration, _tokenConfiguration, _userRepository);
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
}