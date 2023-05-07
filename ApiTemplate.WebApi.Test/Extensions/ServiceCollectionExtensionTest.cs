using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.WebApi.Configs;
using ApiTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApiTemplate.WebApi.Test.Extensions;

public class ServiceCollectionExtensionTest
{
    private readonly ConfigurationManager _configuration;
    private readonly IServiceCollection _serviceCollection;

    public ServiceCollectionExtensionTest()
    {
        var builder = WebApplication.CreateBuilder();
        _serviceCollection = builder.Services;
        _configuration = builder.Configuration;
    }

    [Fact]
    public void AddServices_AddJwtService()
    {
        var tokenConfigurations = new TokenConfiguration();
        new ConfigureFromConfigurationOptions<TokenConfiguration>(_configuration.GetSection("TokenConfiguration"))
            .Configure(tokenConfigurations);
        _serviceCollection.AddJwtService(tokenConfigurations);
        Assert.True(true);
    }

    [Fact]
    public void AddServices_AddDataProviders()
    {
        _serviceCollection.AddDataProviders();
        Assert.True(true);
    }

    [Fact]
    public void ServiceCollectionExtension_ConfigureDefaultErrorHandler()
    {
        _serviceCollection.ConfigureDefaultErrorHandler();
        Assert.True(true);
    }

    [Fact]
    public void ServiceCollectionExtension_ConfigureSwagger()
    {
        var apiDescriptionConfig = new ApiDescriptionConfig();
        new ConfigureFromConfigurationOptions<ApiDescriptionConfig>(_configuration.GetSection("ApiDescription"))
            .Configure(apiDescriptionConfig);

        _serviceCollection.ConfigureSwagger(apiDescriptionConfig);
        Assert.True(true);
    }
}
