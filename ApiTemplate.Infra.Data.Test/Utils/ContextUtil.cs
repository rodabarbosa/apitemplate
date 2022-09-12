using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data.Test.Utils;

static public class ContextUtil
{
    public static ApiTemplateContext GetContext()
    {
        var builder = new DbContextOptionsBuilder<ApiTemplateContext>();
        builder.UseInMemoryDatabase("ApiTemplate");
        return new ApiTemplateContext(builder.Options);
    }
}
