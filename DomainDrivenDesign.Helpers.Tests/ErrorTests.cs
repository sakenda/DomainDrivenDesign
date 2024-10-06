using DomainDrivenDesign.Helpers.Results;

namespace DomainDrivenDesign.Helpers.Tests;

public class ErrorTests
{
    [Fact]
    public void Error_ShouldCreateErrorObjectWithGivenCodeAndMessage()
    {
        // Arrange
        var code = "TestError";
        var message = "Test error message";

        // Act
        var error = new Error(code, message);

        // Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
    }

    [Fact]
    public void Errors_WithSameCodeAndMessage_ShouldBeEqual()
    {
        // Arrange
        var error1 = new Error("TestError", "Test error message");
        var error2 = new Error("TestError", "Test error message");

        // Act & Assert
        Assert.Equal(error1, error2);
    }

    [Fact]
    public void Errors_WithDifferentCodesOrMessages_ShouldNotBeEqual()
    {
        // Arrange
        var error1 = new Error("TestError1", "Test error message");
        var error2 = new Error("TestError2", "Different error message");

        // Act & Assert
        Assert.NotEqual(error1, error2);
    }

    [Fact]
    public void ImplicitConversion_ShouldReturnErrorCodeAsString()
    {
        // Arrange
        var error = new Error("TestError", "Test error message");

        // Act
        string errorCode = error;

        // Assert
        Assert.Equal("TestError", errorCode);
    }

    [Fact]
    public void None_ShouldReturnAnEmptyError()
    {
        // Act
        var error = Error.None;

        // Assert
        Assert.Equal(string.Empty, error.Code);
        Assert.Equal(string.Empty, error.Message);
    }
}
