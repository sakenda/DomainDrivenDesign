namespace DomainDrivenDesign.Helpers.Mappers;

/// <summary>
/// Abstrakte Klasse, die das Mapping zwischen zwei Eingabewerten (TInput1 und TInput2)
/// in einen einzelnen Ausgabewert (TOutput) ermöglicht. Diese Klasse erbt von
/// <see cref="BaseMapper{TInput, TOutput}"/> und definiert spezifische Mapping-Logik
/// für den Fall, dass zwei Eingaben erforderlich sind.
/// </summary>
/// <typeparam name="TInput1">Der Typ des ersten Eingabewerts.</typeparam>
/// <typeparam name="TInput2">Der Typ des zweiten Eingabewerts.</typeparam>
/// <typeparam name="TOutput">Der Typ des Ausgabewerts.</typeparam>
public class DualInputToSingleOutputMapper<TInput1, TInput2, TOutput> : BaseMapper<(TInput1, TInput2), TOutput>
{
    private readonly Func<TInput1, TInput2, TOutput> _outputMappingFunc;

    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="DualInputToSingleOutputMapper{TInput1, TInput2, TOutput}"/>-Klasse
    /// mit der angegebenen Mapping-Funktion.
    /// </summary>
    /// <param name="outputMappingFunc">Eine Funktion, die zwei Eingabewerte in einen Ausgabewert umwandelt.</param>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn <paramref name="outputMappingFunc"/> null ist.</exception>
    public DualInputToSingleOutputMapper(Func<TInput1, TInput2, TOutput> outputMappingFunc)
    {
        _outputMappingFunc = outputMappingFunc ?? throw new ArgumentNullException(nameof(outputMappingFunc));
    }

    /// <summary>
    /// Führt das Mapping von einem Tuple bestehend aus zwei Eingabewerten (TInput1 und TInput2) zu einem Ausgabewert (TOutput) durch.
    /// Diese Methode verwendet die bereitgestellte Mapping-Funktion.
    /// </summary>
    /// <param name="inputValue">Ein Tuple bestehend aus dem ersten und dem zweiten Eingabewert.</param>
    /// <returns>Der gemappte Ausgabewert.</returns>
    protected override TOutput DefaultMapToOutput((TInput1, TInput2) inputValue)
    {
        return _outputMappingFunc(inputValue.Item1, inputValue.Item2);
    }

    /// <summary>
    /// Diese Methode ist nicht implementiert, da das Mapping von Ausgabewerten zu Eingabewerten nicht definiert ist.
    /// </summary>
    /// <param name="outputValue">Der Ausgabewert, der in Eingabewerte umgewandelt werden soll.</param>
    /// <returns>Diese Methode wirft eine <see cref="NotImplementedException"/>.</returns>
    protected override (TInput1, TInput2) DefaultMapToInput(TOutput outputValue)
    {
        throw new NotImplementedException("Mapping from output to input is not defined.");
    }

    /// <summary>
    /// Mappt zwei Listen von Eingabewerten (TInput1 und TInput2) zu einer Liste von Ausgabewerten (TOutput).
    /// Diese Methode stellt sicher, dass die beiden Listen die gleiche Anzahl von Elementen enthalten.
    /// </summary>
    /// <param name="inputValueList1">Die Liste der ersten Eingabewerte.</param>
    /// <param name="inputValueList2">Die Liste der zweiten Eingabewerte.</param>
    /// <returns>Eine Liste von gemappten Ausgabewerten.</returns>
    /// <exception cref="ArgumentNullException">Wird ausgelöst, wenn eine der Listen null ist.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Wird ausgelöst, wenn die Listen unterschiedliche Längen haben.</exception>
    public IEnumerable<TOutput> MapToOutputModels(IEnumerable<TInput1> inputValueList1, IEnumerable<TInput2> inputValueList2)
    {
        ArgumentNullException.ThrowIfNull(inputValueList1);
        ArgumentNullException.ThrowIfNull(inputValueList2);
        ArgumentOutOfRangeException.ThrowIfNotEqual(inputValueList1.Count(), inputValueList2.Count());

        var outputValues = new List<TOutput>();
        using var enumerator1 = inputValueList1.GetEnumerator();
        using var enumerator2 = inputValueList2.GetEnumerator();

        while (enumerator1.MoveNext() && enumerator2.MoveNext())
        {
            var outputValue = MapToOutput((enumerator1.Current, enumerator2.Current));
            outputValues.Add(outputValue);
        }

        return outputValues;
    }
}
