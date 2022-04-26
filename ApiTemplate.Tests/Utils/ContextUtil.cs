using ApiTemplate.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Tests.Utils;

public class ContextUtil
{
    public static ApiTemplateContext GetContext()
    {
        var builder = new DbContextOptionsBuilder<ApiTemplateContext>();
        builder.UseInMemoryDatabase("ApiTemplate");
        return new ApiTemplateContext(builder.Options);
    }
}
