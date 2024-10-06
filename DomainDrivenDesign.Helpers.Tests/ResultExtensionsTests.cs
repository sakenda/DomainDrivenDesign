using DomainDrivenDesign.Helpers.Results;

namespace DomainDrivenDesign.Helpers.Tests;

public class ResultExtensionsTests
{
    [Fact]
    public void WhenListHasEntries_ShouldReturnFailure_WhenListIsNull()
    {
        // Arrange
        var result = Result<IEnumerable<int>>.Success(null);

        // Act
        var actualResult = result.WhenListHasEntries();

        // Assert
        Assert.True(actualResult.IsFailure);
        Assert.Equal(Error.NullValue, actualResult.Error);
    }

    [Fact]
    public void WhenListHasEntries_ShouldReturnFailure_WhenListIsEmpty()
    {
        // Arrange
        var result = Result<IEnumerable<int>>.Success(new List<int>());

        // Act
        var actualResult = result.WhenListHasEntries();

        // Assert
        Assert.True(actualResult.IsFailure);
        Assert.Equal(Error.ListHasNoEntries, actualResult.Error);
    }

    [Fact]
    public void WhenListHasEntries_ShouldReturnSuccess_WhenListHasEntries()
    {
        // Arrange
        var list = new List<int> { 1, 2, 3 };
        var result = Result<IEnumerable<int>>.Success(list);

        // Act
        var actualResult = result.WhenListHasEntries();

        // Assert
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(list, actualResult.Value);
    }

    [Fact]
    public void WithError_ShouldReturnOriginalResult_WhenResultIsSuccess()
    {
        // Arrange
        var result = Result<int>.Success(42);
        var error = new Error("Error.Code", "Error message");

        // Act
        var actualResult = result.WithError(error);

        // Assert
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(42, actualResult.Value);
    }

    [Fact]
    public void WithError_ShouldReturnFailureResultWithNewError_WhenResultIsFailure()
    {
        // Arrange
        var originalError = new Error("OriginalError", "Original message");
        var result = Result<int>.Failure(originalError);
        var newError = new Error("NewError", "New message");

        // Act
        var actualResult = result.WithError(newError);

        // Assert
        Assert.True(actualResult.IsFailure);
        Assert.Equal(newError, actualResult.Error);
    }

    [Fact]
    public void Combine_ShouldReturnFirstFailure_WhenFirstResultIsFailure()
    {
        // Arrange
        var firstError = new Error("FirstError", "First failure");
        var firstResult = Result<int>.Failure(firstError);
        var secondResult = Result<int>.Success(42);

        // Act
        var actualResult = firstResult.Combine(secondResult);

        // Assert
        Assert.True(actualResult.IsFailure);
        Assert.Equal(firstError, actualResult.Error);
    }

    [Fact]
    public void Combine_ShouldReturnSecondFailure_WhenSecondResultIsFailure()
    {
        // Arrange
        var secondError = new Error("SecondError", "Second failure");
        var firstResult = Result<int>.Success(42);
        var secondResult = Result<int>.Failure(secondError);

        // Act
        var actualResult = firstResult.Combine(secondResult);

        // Assert
        Assert.True(actualResult.IsFailure);
        Assert.Equal(secondError, actualResult.Error);
    }

    [Fact]
    public void Combine_ShouldReturnFirstResult_WhenBothResultsAreSuccess()
    {
        // Arrange
        var firstResult = Result<int>.Success(42);
        var secondResult = Result<int>.Success(100);

        // Act
        var actualResult = firstResult.Combine(secondResult);

        // Assert
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(42, actualResult.Value);
    }

    [Fact]
    public void Map_ShouldTransformValue_WhenResultIsSuccess()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var mappedResult = result.Map(x => x.ToString());

        // Assert
        Assert.True(mappedResult.IsSuccess);
        Assert.Equal("42", mappedResult.Value);
    }

    [Fact]
    public void Map_ShouldReturnFailure_WhenResultIsFailure()
    {
        // Arrange
        var error = new Error("SomeError", "Error message");
        var result = Result<int>.Failure(error);

        // Act
        var mappedResult = result.Map(x => x.ToString());

        // Assert
        Assert.True(mappedResult.IsFailure);
        Assert.Equal(error, mappedResult.Error);
    }

    [Fact]
    public void Validate_ShouldReturnFailure_WhenValidationFails()
    {
        // Arrange
        var result = Result<int>.Success(42);
        var validationError = new Error("ValidationError", "Validation failed");

        // Act
        var actualResult = result.Validate(x => x > 100, validationError);

        // Assert
        Assert.True(actualResult.IsFailure);
        Assert.Equal(validationError, actualResult.Error);
    }

    [Fact]
    public void Validate_ShouldReturnOriginalResult_WhenValidationSucceeds()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var actualResult = result.Validate(x => x < 100, new Error("ValidationError", "Validation failed"));

        // Assert
        Assert.True(actualResult.IsSuccess);
        Assert.Equal(42, actualResult.Value);
    }

    [Fact]
    public void Then_ShouldExecuteFunction_WhenResultIsSuccess()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var finalResult = result.Then(() => Result<int>.Success(100));

        // Assert
        Assert.True(finalResult.IsSuccess);
        Assert.Equal(100, finalResult.Value);
    }

    [Fact]
    public void Then_ShouldReturnOriginalResult_WhenResultIsFailure()
    {
        // Arrange
        var error = new Error("SomeError", "Error message");
        var result = Result<int>.Failure(error);

        // Act
        var finalResult = result.Then(() => Result<int>.Success(100));

        // Assert
        Assert.True(finalResult.IsFailure);
        Assert.Equal(error, finalResult.Error);
    }
}
