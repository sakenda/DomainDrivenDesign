using DomainDrivenDesign.Helpers.Results;

namespace DomainDrivenDesign.Helpers.Tests;

public class ResultTests
{
    [Fact]
    public void Success_ShouldReturnSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Failure_ShouldReturnFailureResult()
    {
        // Arrange
        var error = new Error("TestError", "Test error message");

        // Act
        var result = Result.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Success_WithValue_ShouldReturnSuccessResultWithValue()
    {
        // Arrange
        var value = "testValue";

        // Act
        var result = Result<string>.Success(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Failure_WithValue_ShouldReturnFailureResultWithError()
    {
        // Arrange
        var error = new Error("TestError", "Test error message");

        // Act
        var result = Result<string>.Failure(error);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Null(result.Value);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void Create_WithNullValue_ShouldReturnFailureResult()
    {
        // Act
        var result = Result<string>.Create(null);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal(Error.NullValue, result.Error);
    }

    [Fact]
    public void Create_WithNonNullValue_ShouldReturnSuccessResult()
    {
        // Arrange
        var value = "testValue";

        // Act
        var result = Result<string>.Create(value);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(value, result.Value);
    }
}