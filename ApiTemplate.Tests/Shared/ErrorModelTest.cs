using System;
using ApiTemplate.Shared.Models;
using Xunit;

namespace ApiTemplate.Tests.Shared;

public class ErrorModelTest
{
    [Fact]
    public void ErrorModel_Should_Be_Created()
    {
        var errorModel = new ErrorModel
        {
            Code = 0,
            Error = null,
            Exception = null,
            StackTrace = null
        };

        Assert.NotNull(errorModel);
    }

    [Fact]
    public void ErrorModel_Should_Be_Created_With_Exception()
    {
        var exception = new Exception("Test");
        var errorModel = new ErrorModel
        {
            Code = 0,
            Error = exception.Message,
            Exception = exception.GetType().Name,
            StackTrace = exception.StackTrace
        };

        Assert.NotNull(errorModel);
    }

    [Fact]
    public void ErrorModel_Should_Not_BeEmpty()
    {
        var errorModel = new ErrorModel
        {
            Code = 1,
            Error = "Error test",
            Exception = "Exception test",
            StackTrace = "stack trace test"
        };

        Assert.True(errorModel.Code > 0
                    && errorModel.Error != null
                    && !string.IsNullOrEmpty(errorModel.Exception)
                    && !string.IsNullOrEmpty(errorModel.StackTrace));
    }
}
