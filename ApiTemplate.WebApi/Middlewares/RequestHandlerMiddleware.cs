using ApiTemplate.Shared.Exceptions;
using ApiTemplate.Shared.Extensions;
using ApiTemplate.Shared.Models;
using System.Net;

namespace ApiTemplate.WebApi.Middlewares;

/// <summary>
/// This class is used to handle exceptions and return a response with the exception message.
/// </summary>
public sealed class RequestHandlerMiddleware
{
    private const string MediaType = "application/json";
    private const string UnauthorizedMessage = "Unauthorized access";
    private readonly RequestDelegate _next;

    /// <summary>
    /// ErrorHandlingMiddleware
    /// </summary>
    /// <param name="next"></param>
    public RequestHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

#pragma warning disable IDE1006 // Naming Styles

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public async Task Invoke(HttpContext context)
#pragma warning restore IDE1006 // Naming Styles
    {
        try
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException(UnauthorizedMessage);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = exception switch
        {
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            NotFoundException => HttpStatusCode.NotFound,
            DeleteFailureException => HttpStatusCode.BadRequest,
            ForbiddenException => HttpStatusCode.Forbidden,
            NotificationFailureException => HttpStatusCode.BadRequest,
            SaveFailureException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = MediaType;
        context.Response.StatusCode = (int)code;
        var error = new ErrorModel
        {
            Code = (int)code,
            Error = exception.Message,
            Exception = exception.GetType().Name,
            StackTrace = exception.StackTrace
        };

        return context.Response.WriteAsync(error.ToJson());
    }
}
