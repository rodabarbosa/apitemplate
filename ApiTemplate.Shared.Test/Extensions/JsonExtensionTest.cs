namespace ApiTemplate.Shared.Test.Extensions;

public sealed class JsonExtensionTest
{
    private const string JsonTest = "{\"code\":1,\"error\":\"Test Error\",\"exception\":\"Test Exception\",\"stacktrace\":\"Test StackTrace\"}";

    [Fact]
    public void ToJson_Should_Return_Json_String()
    {
        var testObject = new ErrorModel
        {
            Code = 1,
            Error = "Test Error",
            Exception = "Test Exception",
            StackTrace = "Test StackTrace"
        };

        var json = testObject.ToJson();

        json.Should()
            .Be(JsonTest);
    }

    [Fact]
    public void FromJson_Should_Return_Object()
    {
        var testObject = JsonTest.FromJson<ErrorModel>();

        testObject.Should()
            .NotBeNull();

        testObject!.Code
            .Should()
            .Be(1);

        testObject!.Exception
            .Should()
            .Be("Test Exception");

        testObject!.StackTrace
            .Should()
            .Be("Test StackTrace");
    }
}
