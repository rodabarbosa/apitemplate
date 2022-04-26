using ApiTemplate.Shared.Extensions;
using ApiTemplate.Shared.Models;
using Xunit;

namespace ApiTemplate.Tests.SharedProjectTest.Extensions;

public class JsonExtensionTest
{
    private static readonly string JsonTest = "{\"Code\":1,\"Error\":\"Test Error\",\"Exception\":\"Test Exception\",\"StackTrace\":\"Test StackTrace\"}";

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

        Assert.Equal(JsonTest, json);
    }

    [Fact]
    public void FromJson_Should_Return_Object()
    {
        var testObject = JsonTest.FromJson<ErrorModel>();

        Assert.Equal(1, testObject.Code);
        Assert.Equal("Test Error", testObject.Error.ToString());
        Assert.Equal("Test Exception", testObject.Exception);
        Assert.Equal("Test StackTrace", testObject.StackTrace);
    }
}