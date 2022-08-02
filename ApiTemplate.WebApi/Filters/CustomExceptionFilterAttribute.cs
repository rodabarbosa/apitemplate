using System.Net;
using ApiTemplate.Shared.Exceptions;
using ApiTemplate.Shared.Extensions;
using ApiTemplate.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiTemplate.WebApi.Filters;

/// <summary>
/// This class is used to handle exceptions thrown by the application.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private const string MediaType = "application/json";

    /// <summary>
    ///    Called after an action has thrown an <see cref="Exception" />.
    /// </summary>
    /// <param name="context"></param>
    public override void OnException(ExceptionContext context)
    {
        object content = new { message = context.Exception.Message };
        HttpStatusCode code;
        switch (context.Exception.GetType().Name)
        {
            case nameof(DbRegisterExistsException):
            case nameof(DeleteFailureException):
            case nameof(SaveFailureException):
                code = HttpStatusCode.BadRequest;
                break;

            case nameof(ForbiddenException):
                code = HttpStatusCode.Forbidden;
                break;

            case nameof(NotFoundException):
                code = HttpStatusCode.NotFound;
                break;

            case nameof(NotificationFailureException):
                code = HttpStatusCode.InternalServerError;
                break;

            case nameof(UnauthorizedException):
            case nameof(UnauthorizedAccessException):
                code = HttpStatusCode.Unauthorized;
                break;

            case nameof(ValidationException):
                code = HttpStatusCode.NotAcceptable;
                content = ((ValidationException)context.Exception).Failures;
                break;

            default:
                code = HttpStatusCode.BadRequest;
                content = new
                {
                    message = "Something is wrong. Your Request could not be processed.",
                    exception = context.Exception.Message
                };
                break;
        }

        BuildContext(context, (int)code, content);
    }

    private static void BuildContext(ExceptionContext context, int code, object content)
    {
        context.HttpContext.Response.ContentType = MediaType;
        context.HttpContext.Response.StatusCode = code;
        var response = new ErrorModel
        {
            Code = code,
            Error = content,
            Exception = context.Exception.GetType().Name,
            StackTrace = context.Exception.StackTrace
        };

        context.Result = new ContentResult
        {
            StatusCode = code,
            ContentType = MediaType,
            Content = response.ToJson()
        };
    }
}
