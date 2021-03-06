using ApiTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApiTemplate.Tests.WebApiProjectTest.Extensions;

public class ServiceCollectionExtensionTest
{
    private readonly IServiceCollection _serviceCollection;
    private readonly ConfigurationManager _configuration;

    public ServiceCollectionExtensionTest()
    {
        var builder = WebApplication.CreateBuilder();
        _serviceCollection = builder.Services;
        _configuration = builder.Configuration;
    }

    [Fact]
    public void AddServices_AddJwtService()
    {
        _serviceCollection.AddJwtService(_configuration);
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
        _serviceCollection.ConfigureSwagger();
        Assert.True(true);
    }
}
