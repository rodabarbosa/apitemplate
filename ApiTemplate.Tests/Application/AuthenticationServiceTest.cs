using System;
using System.Threading.Tasks;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services;
using Xunit;

namespace ApiTemplate.Tests.Application;

public class AuthenticationServiceTest : IDisposable
{
    [Fact]
    public async Task Should_Authenticate_User()
    {
        // Arrange
        var signingConfiguration = new SigningConfiguration();
        var tokenConfiguration = new TokenConfiguration {Audience = "Teste", Issuer = "Teste", Seconds = 3600};
        var authenticationService = new AuthenticationService(signingConfiguration, tokenConfiguration);

        // Act
        var result = await authenticationService.Authenticate("admin", "admin@123");

        // Assert
        Assert.True(result.Authenticated);
    }

    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}
