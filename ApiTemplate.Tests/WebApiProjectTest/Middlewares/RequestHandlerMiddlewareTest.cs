using System;
using System.IO;
using System.Threading.Tasks;
using ApiTemplate.Shared.Extensions;
using ApiTemplate.Shared.Models;
using ApiTemplate.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ApiTemplate.Tests.WebApiProjectTest.Middlewares;

public class RequestHandlerMiddlewareTest
{
    [Fact]
    public void Invoke_WhenCalled_ReturnsOk()
    {
        const string expectedOutput = "Request handed over to next request delegate";

        // Arrange
        var defaultContext = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream() },
            Request = { Path = "/" }
        };

        // Act
        var middlewareInstance = new RequestHandlerMiddleware(innerHttpContext =>
        {
            innerHttpContext.Response.WriteAsync(expectedOutput);
            return Task.CompletedTask;
        });

        _ = middlewareInstance.Invoke(defaultContext);

        defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
        Assert.Equal(expectedOutput, body);
    }

    [Fact]
    public void Invoke_WhenCalled_ReturnsException()
    {
        // Arrange
        var defaultContext = new DefaultHttpContext();
        defaultContext.Response.Body = new MemoryStream();
        defaultContext.Request.Path = "/";

        // Act
        var middlewareInstance = new RequestHandlerMiddleware(_ => throw new Exception("TEST Exception"));

        _ = middlewareInstance.Invoke(defaultContext) ?? throw new ArgumentNullException($"middlewareInstance.Invoke(defaultContext)");

        defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
        var response = body.FromJson<ErrorModel>();
        Assert.NotNull(response);
    }
}
