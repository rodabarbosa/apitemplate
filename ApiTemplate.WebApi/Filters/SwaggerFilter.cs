using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiTemplate.WebApi.Filters;

/// <summary>
/// Swagger filter to add the <see cref="ApiVersion"/> to the Swagger document.
/// </summary>
public class SwaggerFilter : IOperationFilter
{
    /// <summary>
    /// Apply the filter to the Swagger document.
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) return;

        foreach (var parameter in operation.Parameters)
        {
            var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
            var routeInfo = description.RouteInfo;

            if (parameter.Description == null)
                parameter.Description = description.ModelMetadata?.Description;

            if (routeInfo == null)
                continue;

            parameter.Required |= !routeInfo.IsOptional;
        }
    }
}
