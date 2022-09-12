using ApiTemplate.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.WebApi.Test.Utils;

static public class ContextUtil
{
    public static ApiTemplateContext GetContext()
    {
        var builder = new DbContextOptionsBuilder<ApiTemplateContext>()
            .UseInMemoryDatabase("ApiTemplate");

        return new ApiTemplateContext(builder.Options);
    }
}
