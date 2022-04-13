using System.Net;
using ApiTemplate.Shared.Extensions;
using ApiTemplate.Shared.Models;

namespace ApiTemplate.WebApi.Middlewares;

public class RequestHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private const string MediaType = "application/json";
    private const string DefaultMessage = "Internal Server Error";
    private const string UnauthorizedMessage = "Unauthorized access";

    /// <summary>
    /// ErrorHandlingMiddleware
    /// </summary>
    /// <param name="next"></param>
    public RequestHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

#pragma warning disable IDE1006 // Naming Styles

    public async Task Invoke(HttpContext context)
#pragma warning restore IDE1006 // Naming Styles
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == (int) HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(UnauthorizedMessage);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        object title = DefaultMessage;
        if (exception is UnauthorizedAccessException)
        {
            code = HttpStatusCode.Unauthorized;
            title = UnauthorizedMessage;
        }

        context.Response.ContentType = MediaType;
        context.Response.StatusCode = (int) code;
        var error = new ErrorModel
        {
            Code = (int) code,
            Error = exception.Message,
            Exception = exception.GetType().Name,
            StackTrace = exception.StackTrace
        };

        return context.Response.WriteAsync(error.ToJson());
    }
}
