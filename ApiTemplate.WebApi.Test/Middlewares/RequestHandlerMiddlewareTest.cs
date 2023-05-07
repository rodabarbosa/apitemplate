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

        body.Should()
            .Be(expectedOutput);
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
        var middlewareInstance = new RequestHandlerMiddleware(_ => Task.CompletedTask);

        _ = middlewareInstance.Invoke(defaultContext);

        defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();

        var response = body.FromJson<ErrorModel>();

        response.Should()
            .NotBeNull();
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

        response.Should()
            .NotBeNull();
    }
}
