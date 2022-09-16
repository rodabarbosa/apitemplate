using ApiTemplate.Shared.Extensions;
using ApiTemplate.Shared.Models;
using ApiTemplate.WebApi.Middlewares;
using Microsoft.AspNetCore.Http;

namespace ApiTemplate.WebApi.Test.Middlewares;

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
    public void Invoke_WhenCalled_ReturnsUnauthorized()
    {
        // Arrange
        var defaultContext = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream(), StatusCode = 401 },
            Request = { Path = "/" }
        };

        // Act
        var middlewareInstance = new RequestHandlerMiddleware(innerHttpContext => Task.CompletedTask);

        _ = middlewareInstance.Invoke(defaultContext);

        defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
        var response = body.FromJson<ErrorModel>();
        Assert.NotNull(response);
    }

    [Theory]
    [InlineData(400)]
    [InlineData(401)]
    public void Invoke_WhenCalled_ReturnsException(int code)
    {
        // Arrange
        var defaultContext = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream(), StatusCode = code },
            Request = { Path = "/" }
        };

        // Act
        var middlewareInstance = new RequestHandlerMiddleware(_ => throw new Exception("TEST Exception"));

        _ = middlewareInstance.Invoke(defaultContext);

        defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
        var response = body.FromJson<ErrorModel>();
        Assert.NotNull(response);
    }
}
