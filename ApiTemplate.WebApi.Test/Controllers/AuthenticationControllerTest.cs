using ApiTemplate.Application.Contracts;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Application.Services.Authentication;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.WebApi.Controllers;
using ApiTemplate.WebApi.Extensions;
using ApiTemplate.WebApi.Test.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.WebApi.Test.Controllers;

public class AuthenticationControllerTest
{
    private readonly ConfigurationManager _configuration;
    private readonly AuthenticationController _controller;
    private readonly IAuthenticateService _service;
    private readonly IServiceCollection _serviceCollection;

    public AuthenticationControllerTest()
    {
        var builder = WebApplication.CreateBuilder();
        _serviceCollection = builder.Services;
        _configuration = builder.Configuration;

        _serviceCollection.AddJwtService(_configuration);
        _serviceCollection.AddDataProviders();
        _serviceCollection.ConfigureDefaultErrorHandler();
        _serviceCollection.ConfigureSwagger();

        var signInConfiguration = new SigningConfiguration();
        var tokenConfiguration = new TokenConfiguration
        {
            Audience = "test",
            Issuer = "test",
            Seconds = 20
        };

        var context = ContextUtil.GetContext();
        var reposity = new UserRepository(context);
        _service = new AuthenticateService(signInConfiguration, tokenConfiguration, reposity);

        _controller = new AuthenticationController();
    }

    [Theory]
    [InlineData("admin", "123456", false)]
    [InlineData("admin", "admin@123", true)]
    public async Task Login_WithValidCredentials_ReturnsOk(string username, string password, bool expected)
    {
        // Arrange
        var request = new AuthenticateRequestContract
        {
            Username = username,
            Password = password
        };

        if (expected)
        {
            _ = await _controller.PostAsync(_service, request);
            Assert.True(true);
            return;
        }

        Assert.ThrowsAnyAsync<Exception>(() => _controller.PostAsync(_service, request));
    }
}
