using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiTemplate.WebApi.Filters;

public class SwaggerFilter : IOperationFilter
{
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
