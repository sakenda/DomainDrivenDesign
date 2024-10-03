namespace DomainDrivenDesign.Helpers.Mappers;

/// <summary>
/// Ein Mapper, der einen Eingabewert in zwei verschiedene Ausgabewerte umwandelt.
/// Diese Klasse verwendet eine benutzerdefinierte Mapping-Funktion, die einen Eingabewert als Eingabe nimmt
/// und zwei entsprechende Ausgabewerte zurückgibt.
/// </summary>
/// <typeparam name="TInput">Der Typ des Eingabewerts.</typeparam>
/// <typeparam name="TOutput1">Der Typ des ersten Ausgabewerts.</typeparam>
/// <typeparam name="TOutput2">Der Typ des zweiten Ausgabewerts.</typeparam>
public class SingleInputToDualOutputMapper<TInput, TOutput1, TOutput2>
{
    /// <summary>
    /// Die benutzerdefinierte Mapping-Funktion, die einen Eingabewert in zwei Ausgabewerte umwandelt.
    /// </summary>
    private readonly Func<TInput, (TOutput1, TOutput2)> _outputMappingFunc;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="SingleInputToDualOutputMapper{TInput, TOutput1, TOutput2}"/> Klasse.
    /// </summary>
    /// <param name="outputMappingFunc">Die Funktion, die die Logik für die Umwandlung enthält.</param>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn die übergebene Funktion null ist.</exception>
    public SingleInputToDualOutputMapper(Func<TInput, (TOutput1, TOutput2)> outputMappingFunc)
    {
        _outputMappingFunc = outputMappingFunc ?? throw new ArgumentNullException(nameof(outputMappingFunc));
    }

    /// <summary>
    /// Mappt einen Eingabewert auf zwei Ausgabewerte.
    /// </summary>
    /// <param name="inputValue">Der Eingabewert, der umgewandelt wird.</param>
    /// <returns>Ein Tupel mit den zwei Ausgabewerten.</returns>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn der Eingabewert null ist.</exception>
    public (TOutput1, TOutput2) MapToOutputModels(TInput inputValue)
    {
        ArgumentNullException.ThrowIfNull(inputValue);
        return _outputMappingFunc(inputValue);
    }

    /// <summary>
    /// Mappt eine Liste von Eingabewerten auf zwei Listen von Ausgabewerten.
    /// </summary>
    /// <param name="inputValueList">Die Liste der Eingabewerte.</param>
    /// <returns>Ein Tupel mit zwei Listen von Ausgabewerten.</returns>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn die Liste der Eingabewerte null ist.</exception>
    public (IEnumerable<TOutput1>, IEnumerable<TOutput2>) MapToOutputModels(IEnumerable<TInput> inputValueList)
    {
        ArgumentNullException.ThrowIfNull(inputValueList);

        var outputList1 = new List<TOutput1>();
        var outputList2 = new List<TOutput2>();

        foreach (var inputValue in inputValueList)
        {
            var (outputValue1, outputValue2) = MapToOutputModels(inputValue);
            outputList1.Add(outputValue1);
            outputList2.Add(outputValue2);
        }

        return (outputList1, outputList2);
    }
}
