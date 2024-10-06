using DomainDrivenDesign.Helpers.Mappers;

namespace DomainDrivenDesign.Helpers.Tests;

public class BaseMapperTests
{
    public class StringToIntMapper : BaseMapper<string, int>
    {
        protected override int DefaultMapToOutput(string inputValue)
        {
            return inputValue.Length;
        }

        protected override string DefaultMapToInput(int outputValue)
        {
            return new string('a', outputValue);
        }
    }

    [Fact]
    public void MapToOutput_WithValidInput_UsesDefaultMapping()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        string input = "test";

        // Act
        var result = mapper.MapToOutput(input);

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void MapToInput_WithValidInput_UsesDefaultMapping()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        int input = 5;

        // Act
        var result = mapper.MapToInput(input);

        // Assert
        Assert.Equal("aaaaa", result);
    }

    [Fact]
    public void MapToOutput_WithCustomMapping_UsesCustomMapping()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        mapper.CustomOutputMapping = input => input.Length * 2; // Custom logic: length * 2
        string input = "test";

        // Act
        var result = mapper.MapToOutput(input);

        // Assert
        Assert.Equal(8, result);
    }

    [Fact]
    public void MapToInput_WithCustomMapping_UsesCustomMapping()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        mapper.CustomInputMapping = output => new string('b', output); // Custom logic: 'b' repeated output times
        int input = 3;

        // Act
        var result = mapper.MapToInput(input);

        // Assert
        Assert.Equal("bbb", result);
    }

    [Fact]
    public void MapToOutput_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        string nullString = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutput(nullString));
    }

    [Fact]
    public void MapToOutputs_WithValidList_MapsAllItems()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        var inputList = new List<string> { "a", "bb", "ccc" };

        // Act
        var result = mapper.MapToOutputs(inputList).ToList();

        // Assert
        Assert.Equal(new List<int> { 1, 2, 3 }, result);
    }

    [Fact]
    public void MapToInputs_WithValidList_MapsAllItems()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        var inputList = new List<int> { 1, 2, 3 };

        // Act
        var result = mapper.MapToInputs(inputList).ToList();

        // Assert
        Assert.Equal(new List<string> { "a", "aa", "aaa" }, result);
    }

    [Fact]
    public void MapToOutputs_WithNullList_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new StringToIntMapper();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutputs(null));
    }

    [Fact]
    public void MapToInputs_WithNullList_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new StringToIntMapper();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToInputs(null));
    }

    [Fact]
    public void MapToOutputs_WithCustomMapping_AppliesCustomLogic()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        mapper.CustomOutputMapping = input => input.Length + 10; // Custom logic: length + 10
        var inputList = new List<string> { "a", "bb", "ccc" };

        // Act
        var result = mapper.MapToOutputs(inputList).ToList();

        // Assert
        Assert.Equal(new List<int> { 11, 12, 13 }, result);
    }

    [Fact]
    public void MapToInputs_WithCustomMapping_AppliesCustomLogic()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        mapper.CustomInputMapping = output => new string('z', output); // Custom logic: 'z' repeated output times
        var inputList = new List<int> { 1, 2, 3 };

        // Act
        var result = mapper.MapToInputs(inputList).ToList();

        // Assert
        Assert.Equal(new List<string> { "z", "zz", "zzz" }, result);
    }

    [Fact]
    public void MapToOutputs_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        var emptyList = new List<string>();

        // Act
        var result = mapper.MapToOutputs(emptyList);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void MapToInputs_WithEmptyList_ReturnsEmptyList()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        var emptyList = new List<int>();

        // Act
        var result = mapper.MapToInputs(emptyList);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void MapToOutputs_WithNullItemInList_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new StringToIntMapper();
        var inputList = new List<string> { "a", null, "ccc" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutputs(inputList));
    }

}
