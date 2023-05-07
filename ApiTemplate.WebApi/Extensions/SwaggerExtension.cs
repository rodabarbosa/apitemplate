using ApiTemplate.WebApi.Configs;
using ApiTemplate.WebApi.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace ApiTemplate.WebApi.Extensions;

/// <summary>
/// ServiceCollection Extensions
/// </summary>
static public class SwaggerExtension
{
    /// <summary>
    ///  Add swagger configuration to services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="apiDescription"></param>
    static public void ConfigureSwagger(this IServiceCollection services, ApiDescriptionConfig apiDescription)
    {
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning();

        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = !string.IsNullOrEmpty(apiDescription.Title) ? $"{apiDescription.Title} {description.ApiVersion}" : default,
                    Version = description.ApiVersion.ToString(),
                    Description = !string.IsNullOrEmpty(apiDescription.Description) ? apiDescription.Description : default,
                    TermsOfService = !string.IsNullOrEmpty(apiDescription.TermOfService) ? new Uri(apiDescription.TermOfService) : default,
                    Contact = new OpenApiContact
                    {
                        Email = !string.IsNullOrEmpty(apiDescription.ContactEmail) ? apiDescription.ContactEmail : default,
                        Name = !string.IsNullOrEmpty(apiDescription.ContactName) ? apiDescription.ContactName : default,
                        Url = !string.IsNullOrEmpty(apiDescription.ContactSite) ? new Uri(apiDescription.ContactSite) : default
                    },
                    License = default
                });
            options.OperationFilter<SecurityRequirementsOperationFilter>();
            options.OperationFilter<SwaggerFilter>();

            var assemblyName = Assembly.GetEntryAssembly()!
                .GetName()
                .Name;

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml"));

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "Authorization token using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Type = SecuritySchemeType.Http
            });
        });
    }
}
