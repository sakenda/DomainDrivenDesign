namespace DomainDrivenDesign.Helpers.Mappers;

/// <summary>
/// Abstrakte Basisklasse für das Mapping zwischen zwei generischen Typen.
/// Ermöglicht die Anpassung der Mapping-Logik durch Delegaten und definiert Standardmethoden für das Mapping
/// von Einzelobjekten und Listen.
/// </summary>
/// <typeparam name="TInput">Der Typ des Eingabewerts.</typeparam>
/// <typeparam name="TOutput">Der Typ des Ausgabewerts.</typeparam>
public abstract class BaseMapper<TInput, TOutput>
{
    /// <summary>
    /// Eine benutzerdefinierte Mapping-Funktion, die einen Eingabewert in einen Ausgabewert umwandelt.
    /// Wenn festgelegt, wird diese Funktion verwendet, anstatt die Standard-Mapping-Logik anzuwenden.
    /// </summary>
    public Func<TInput, TOutput>? CustomOutputMapping { get; set; }

    /// <summary>
    /// Eine benutzerdefinierte Mapping-Funktion, die einen Ausgabewert in einen Eingabewert umwandelt.
    /// Wenn festgelegt, wird diese Funktion verwendet, anstatt die Standard-Mapping-Logik anzuwenden.
    /// </summary>
    public Func<TOutput, TInput>? CustomInputMapping { get; set; }

    /// <summary>
    /// Mappt einen Eingabewert auf einen Ausgabewert. Wenn eine benutzerdefinierte Mapping-Funktion definiert ist,
    /// wird diese verwendet, andernfalls wird die standardmäßige Mapping-Methode aufgerufen.
    /// </summary>
    /// <param name="inputValue">Der Eingabewert, der in einen Ausgabewert umgewandelt werden soll.</param>
    /// <returns>Der gemappte Ausgabewert.</returns>
    public virtual TOutput MapToOutput(TInput inputValue)
    {
        ArgumentNullException.ThrowIfNull(inputValue);
        return CustomOutputMapping != null ? CustomOutputMapping(inputValue) : DefaultMapToOutput(inputValue);
    }

    /// <summary>
    /// Mappt einen Ausgabewert auf einen Eingabewert. Wenn eine benutzerdefinierte Mapping-Funktion definiert ist,
    /// wird diese verwendet, andernfalls wird die standardmäßige Mapping-Methode aufgerufen.
    /// </summary>
    /// <param name="outputValue">Der Ausgabewert, der in einen Eingabewert umgewandelt werden soll.</param>
    /// <returns>Der gemappte Eingabewert.</returns>
    public virtual TInput MapToInput(TOutput outputValue)
    {
        ArgumentNullException.ThrowIfNull(outputValue);
        return CustomInputMapping != null ? CustomInputMapping(outputValue) : DefaultMapToInput(outputValue);
    }

    /// <summary>
    /// Abstrakte Methode zur Implementierung der Standard-Mapping-Logik von einem Eingabewert zu einem Ausgabewert.
    /// Diese Methode muss in einer abgeleiteten Klasse implementiert werden, um das spezifische Mapping zu definieren.
    /// </summary>
    /// <param name="inputValue">Der Eingabewert, der in einen Ausgabewert umgewandelt werden soll.</param>
    /// <returns>Der gemappte Ausgabewert.</returns>
    protected abstract TOutput DefaultMapToOutput(TInput inputValue);

    /// <summary>
    /// Abstrakte Methode zur Implementierung der Standard-Mapping-Logik von einem Ausgabewert zu einem Eingabewert.
    /// Diese Methode muss in einer abgeleiteten Klasse implementiert werden, um das spezifische Mapping zu definieren.
    /// </summary>
    /// <param name="outputValue">Der Ausgabewert, der in einen Eingabewert umgewandelt werden soll.</param>
    /// <returns>Der gemappte Eingabewert.</returns>
    protected abstract TInput DefaultMapToInput(TOutput outputValue);

    /// <summary>
    /// Mappt eine Liste von Eingabewerten auf eine Liste von Ausgabewerten.
    /// Standardmäßig wird die Mapping-Logik für jeden Eingabewert in der Liste angewendet.
    /// </summary>
    /// <param name="inputValueList">Eine Liste von Eingabewerten, die in Ausgabewerte umgewandelt werden sollen.</param>
    /// <returns>Eine Liste von gemappten Ausgabewerten.</returns>
    public virtual IEnumerable<TOutput> MapToOutputs(IEnumerable<TInput> inputValueList)
    {
        ArgumentNullException.ThrowIfNull(inputValueList);
        return inputValueList.Select(MapToOutput).ToList();
    }

    /// <summary>
    /// Mappt eine Liste von Ausgabewerten auf eine Liste von Eingabewerten.
    /// Standardmäßig wird die Mapping-Logik für jeden Ausgabewert in der Liste angewendet.
    /// </summary>
    /// <param name="outputValueList">Eine Liste von Ausgabewerten, die in Eingabewerte umgewandelt werden sollen.</param>
    /// <returns>Eine Liste von gemappten Eingabewerten.</returns>
    public virtual IEnumerable<TInput> MapToInputs(IEnumerable<TOutput> outputValueList)
    {
        ArgumentNullException.ThrowIfNull(outputValueList);
        return outputValueList.Select(MapToInput).ToList();
    }
}
