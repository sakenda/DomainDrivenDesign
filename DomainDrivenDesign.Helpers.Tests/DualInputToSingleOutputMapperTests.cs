using DomainDrivenDesign.Helpers.Mappers;

namespace DomainDrivenDesign.Helpers.Tests;

public class DualInputToSingleOutputMapperTests
{
    // Beispiel-Mapping-Funktion
    private string ExampleMappingFunction(int input1, string input2)
    {
        return $"{input1}_{input2}";
    }

    // Test für die Konstruktorvalidierung
    [Fact]
    public void Constructor_WithNullMappingFunction_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new DualInputToSingleOutputMapper<int, string, string>(null));
    }

    // Test für korrektes Mapping
    [Fact]
    public void MapToOutput_WithValidInputs_ReturnsExpectedOutput()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>(ExampleMappingFunction);
        var inputValue = (1, "Test");

        // Act
        var result = mapper.MapToOutput(inputValue);

        // Assert
        Assert.Equal("1_Test", result);
    }

    // Test für korrektes Mapping mit Listen
    [Fact]
    public void MapToOutputModels_WithEqualLengthLists_ReturnsMappedOutputs()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>(ExampleMappingFunction);
        var list1 = new List<int> { 1, 2, 3 };
        var list2 = new List<string> { "A", "B", "C" };

        // Act
        var results = mapper.MapToOutputModels(list1, list2);

        // Assert
        var expectedResults = new List<string> { "1_A", "2_B", "3_C" };
        Assert.Equal(expectedResults, results);
    }

    // Test für ArgumentNullException bei null-Listen
    [Fact]
    public void MapToOutputModels_WithNullFirstList_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>(ExampleMappingFunction);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutputModels(null, new List<string> { "A" }));
    }

    [Fact]
    public void MapToOutputModels_WithNullSecondList_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>(ExampleMappingFunction);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutputModels(new List<int> { 1 }, null));
    }

    // Test für ArgumentOutOfRangeException bei unterschiedlichen Listenlängen
    [Fact]
    public void MapToOutputModels_WithDifferentLengthLists_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>(ExampleMappingFunction);
        var list1 = new List<int> { 1, 2 };
        var list2 = new List<string> { "A" };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => mapper.MapToOutputModels(list1, list2));
    }

    // Test für das Verhalten bei einer leeren Liste
    [Fact]
    public void MapToOutputModels_WithEmptyLists_ReturnsEmptyList()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>(ExampleMappingFunction);
        var list1 = new List<int>();
        var list2 = new List<string>();

        // Act
        var results = mapper.MapToOutputModels(list1, list2);

        // Assert
        Assert.Empty(results);
    }

    // Test für die Funktionalität von benutzerdefinierten Mapping-Funktionen
    [Fact]
    public void MapToOutput_WithCustomMappingFunction_ReturnsExpectedOutput()
    {
        // Arrange
        var mapper = new DualInputToSingleOutputMapper<int, string, string>((i1, i2) => $"{i1 * 2} - {i2.ToUpper()}");
        var inputValue = (2, "test");

        // Act
        var result = mapper.MapToOutput(inputValue);

        // Assert
        Assert.Equal("4 - TEST", result);
    }
}
