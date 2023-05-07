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

        var act = () => _serviceCollection.AddJwtService(tokenConfigurations);

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void AddServices_AddDataProviders()
    {
        var act = () => _serviceCollection.AddDataProviders();

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void ServiceCollectionExtension_ConfigureDefaultErrorHandler()
    {
        var act = () => _serviceCollection.ConfigureDefaultErrorHandler();

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void ServiceCollectionExtension_ConfigureSwagger()
    {
        var apiDescriptionConfig = new ApiDescriptionConfig();
        new ConfigureFromConfigurationOptions<ApiDescriptionConfig>(_configuration.GetSection("ApiDescription"))
            .Configure(apiDescriptionConfig);

        var act = () => _serviceCollection.ConfigureSwagger(apiDescriptionConfig);

        act.Should()
            .NotThrow();
    }
}
