using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.WebApi.Configs;
using ApiTemplate.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace ApiTemplate.WebApi.Test.Controllers;

public abstract class BaseControllerTest
{
    public BaseControllerTest()
    {
        var builder = WebApplication.CreateBuilder();
        var serviceCollection = builder.Services;
        var configuration = builder.Configuration;

        var tokenConfigurations = new TokenConfiguration();
        new ConfigureFromConfigurationOptions<TokenConfiguration>(configuration.GetSection("TokenConfiguration"))
            .Configure(tokenConfigurations);

        serviceCollection.AddJwtService(tokenConfigurations);
        serviceCollection.AddDataProviders();
        serviceCollection.ConfigureDefaultErrorHandler();

        var apiDescriptionConfig = new ApiDescriptionConfig();
        new ConfigureFromConfigurationOptions<ApiDescriptionConfig>(configuration.GetSection("ApiDescription"))
            .Configure(apiDescriptionConfig);
        serviceCollection.ConfigureSwagger(apiDescriptionConfig);
    }
}
