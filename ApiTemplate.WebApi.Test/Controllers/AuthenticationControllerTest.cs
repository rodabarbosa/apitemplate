using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services.Authentication;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.WebApi.Controllers;
using ApiTemplate.WebApi.Extensions;
using ApiTemplate.WebApi.Test.Utils;
using Microsoft.AspNetCore.Builder;

namespace ApiTemplate.WebApi.Test.Controllers;

public class AuthenticationControllerTest
{
    private readonly AuthenticationController _controller = new();

    public AuthenticationControllerTest()
    {
        var builder = WebApplication.CreateBuilder();
        var serviceCollection = builder.Services;
        var configuration = builder.Configuration;

        serviceCollection.AddJwtService(configuration);
        serviceCollection.AddDataProviders();
        serviceCollection.ConfigureDefaultErrorHandler();
        serviceCollection.ConfigureSwagger();
    }

    [Theory]
    [InlineData("admin", "123456", false)]
    [InlineData("admin", "admin@123", true)]
    public async Task Login_WithValidCredentials_ReturnsOk(string username, string password, bool expected)
    {
        // Arrange
        var signInConfiguration = new SigningConfiguration();
        var tokenConfiguration = new TokenConfiguration
        {
            Audience = "test",
            Issuer = "test",
            Seconds = 20
        };

        var context = ContextUtil.GetContext();
        var reposity = new UserRepository(context);
        var service = new AuthenticateService(signInConfiguration, tokenConfiguration, reposity);

        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        if (expected)
        {
            _ = await _controller.PostAsync(service, request);
            Assert.True(true);
            return;
        }

        await Assert.ThrowsAnyAsync<Exception>(() => _controller.PostAsync(service, request));
    }
}
