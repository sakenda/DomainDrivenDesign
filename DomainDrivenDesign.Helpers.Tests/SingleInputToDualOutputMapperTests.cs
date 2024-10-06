using DomainDrivenDesign.Helpers.Mappers;
using System.Globalization;

namespace DomainDrivenDesign.Helpers.Tests;

public class SingleInputToDualOutputMapperTests
{
    // Set the culture to invariant to avoid localization issues
    public SingleInputToDualOutputMapperTests()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
    }

    // Beispiel-Mapping-Funktion
    private (string, int) ExampleMappingFunction(double input)
    {
        return ($"Value: {input.ToString(CultureInfo.InvariantCulture)}", (int)(input * 10));
    }

    // Test für die Konstruktorvalidierung
    [Fact]
    public void Constructor_WithNullMappingFunction_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new SingleInputToDualOutputMapper<double, string, int>(null));
    }

    // Test für korrektes Mapping eines einzelnen Wertes
    [Fact]
    public void MapToOutputModels_WithValidInput_ReturnsExpectedOutputs()
    {
        // Arrange
        var mapper = new SingleInputToDualOutputMapper<double, string, int>(ExampleMappingFunction);
        var inputValue = 5.5;

        // Act
        var (output1, output2) = mapper.MapToOutputModels(inputValue);

        // Assert
        Assert.Equal("Value: 5.5", output1);
        Assert.Equal(55, output2);
    }

    // Test für korrektes Mapping einer Liste von Eingabewerten
    [Fact]
    public void MapToOutputModels_WithValidInputList_ReturnsExpectedOutputLists()
    {
        // Arrange
        var mapper = new SingleInputToDualOutputMapper<double, string, int>(ExampleMappingFunction);
        var inputList = new List<double> { 1.0, 2.0, 3.5 };

        // Act
        var (outputList1, outputList2) = mapper.MapToOutputModels(inputList);

        // Assert
        var expectedOutputList1 = new List<string> { "Value: 1", "Value: 2", "Value: 3.5" };
        var expectedOutputList2 = new List<int> { 10, 20, 35 };

        Assert.Equal(expectedOutputList1, outputList1);
        Assert.Equal(expectedOutputList2, outputList2);
    }

    // Test für ArgumentNullException bei null-Input
    [Fact]
    public void MapToOutputModels_WithNullInput_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new SingleInputToDualOutputMapper<double, string, int>(ExampleMappingFunction);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutputModels(null));
    }

    // Test für ArgumentNullException bei null-Input-Liste
    [Fact]
    public void MapToOutputModels_WithNullInputList_ThrowsArgumentNullException()
    {
        // Arrange
        var mapper = new SingleInputToDualOutputMapper<double, string, int>(ExampleMappingFunction);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => mapper.MapToOutputModels(null));
    }

    // Test für das Verhalten bei einer leeren Liste
    [Fact]
    public void MapToOutputModels_WithEmptyList_ReturnsEmptyOutputLists()
    {
        // Arrange
        var mapper = new SingleInputToDualOutputMapper<double, string, int>(ExampleMappingFunction);
        var inputList = new List<double>();

        // Act
        var (outputList1, outputList2) = mapper.MapToOutputModels(inputList);

        // Assert
        Assert.Empty(outputList1);
        Assert.Empty(outputList2);
    }
}
