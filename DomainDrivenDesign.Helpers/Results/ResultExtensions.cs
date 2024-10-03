namespace DomainDrivenDesign.Helpers.Results;

public static class ResultExtensions
{
    /// <summary>
    /// Überprüft, ob das Resultat eine nicht-leere Liste enthält.
    /// Gibt ein fehlgeschlagenes Resultat zurück, wenn die Liste leer ist.
    /// </summary>
    /// <typeparam name="TValue">Der Typ der Listenelemente.</typeparam>
    /// <param name="result">Das Resultat, das eine Liste enthält.</param>
    /// <returns>Ein neues Resultat, das angibt, ob die Liste Einträge enthält.</returns>
    public static Result<IEnumerable<TValue>> WhenListHasEntries<TValue>(this Result<IEnumerable<TValue>> result)
    {
        if (result is null || result.Value is null)
        {
            return Result<IEnumerable<TValue>>.Failure(Error.NullValue);
        }

        if (!result.Value.Any())
        {
            return Result<IEnumerable<TValue>>.Failure(Error.ListHasNoEntries);
        }

        return result;
    }

    /// <summary>
    /// Fügt dem Resultat einen Fehler hinzu, wenn es fehlgeschlagen ist.
    /// Gibt das ursprüngliche Resultat zurück, wenn es erfolgreich war.
    /// </summary>
    /// <typeparam name="TValue">Der Typ des Werts im Resultat.</typeparam>
    /// <param name="result">Das Resultat.</param>
    /// <param name="error">Der Fehler, der hinzugefügt werden soll.</param>
    /// <returns>Ein neues Resultat, das den Fehler enthält, wenn das ursprüngliche Resultat fehlgeschlagen ist, andernfalls das ursprüngliche Resultat.</returns>
    public static Result<TValue> WithError<TValue>(this Result<TValue> result, Error error)
    {
        return result.IsFailure ? Result<TValue>.Failure(error) : result;
    }

    /// <summary>
    /// Kombiniert zwei Resultate. Gibt das erste fehlgeschlagene Resultat zurück oder das erfolgreiche, wenn beide erfolgreich sind.
    /// </summary>
    /// <typeparam name="TValue">Der Typ des Werts im Resultat.</typeparam>
    /// <param name="first">Das erste Resultat.</param>
    /// <param name="second">Das zweite Resultat.</param>
    /// <returns>Das erste fehlgeschlagene Resultat oder das erfolgreiche Resultat.</returns>
    public static Result<TValue> Combine<TValue>(this Result<TValue> first, Result<TValue> second)
    {
        return first.IsFailure ? first : second.IsFailure ? second : first;
    }

    /// <summary>
    /// Wandelt den Wert des Resultats in einen neuen Wert um, wenn das Resultat erfolgreich ist.
    /// </summary>
    /// <typeparam name="TValue">Der Typ des ursprünglichen Werts im Resultat.</typeparam>
    /// <typeparam name="TNewValue">Der Typ des neuen Werts.</typeparam>
    /// <param name="result">Das Resultat mit dem ursprünglichen Wert.</param>
    /// <param name="mapFunc">Eine Funktion, die den ursprünglichen Wert in den neuen Wert umwandelt.</param>
    /// <returns>Ein neues Resultat mit dem umgewandelten Wert, oder ein fehlgeschlagenes Resultat mit dem gleichen Fehler.</returns>
    public static Result<TNewValue> Map<TValue, TNewValue>(this Result<TValue> result, Func<TValue, TNewValue> mapFunc)
    {
        if (result.IsSuccess)
        {
            return Result<TNewValue>.Success(mapFunc(result.Value!));
        }

        return Result<TNewValue>.Failure(result.Error);
    }

    /// <summary>
    /// Wandelt die Werte des Results unter Verwendung einer angegebenen Abbildungsmethode um.
    /// </summary>
    /// <typeparam name="TInput">Der Typ der Eingabewerte.</typeparam>
    /// <typeparam name="TOutput">Der Typ der Ausgabewerte.</typeparam>
    /// <param name="result">Das Result, das die zu konvertierenden Werte enthält.</param>
    /// <param name="mapFunc">Die Abbildungsmethode, die einen Eingabewert in einen Ausgabewert umwandelt.</param>
    /// <returns>Ein Result, das die umgewandelten Werte enthält oder einen Fehler.</returns>
    public static Result<IEnumerable<TOutput>> Map<TInput, TOutput>(this Result<IEnumerable<TInput>> result, Func<TInput, TOutput> mapFunc)
    {
        if (result.IsFailure)
        {
            return Result<IEnumerable<TOutput>>.Failure(result.Error);
        }

        if (result.Value is null)
        {
            return Result<IEnumerable<TOutput>>.Failure(Error.NullValue);
        }

        return Result<IEnumerable<TOutput>>.Success(result.Value.Select(mapFunc).ToList());
    }

    /// <summary>
    /// Führt eine Validierung des Wertes im <see cref="Result{TValue}"/> durch,
    /// basierend auf der angegebenen Validierungsfunktion.
    /// </summary>
    /// <typeparam name="TValue">Der Typ des Wertes, der im Result enthalten ist.</typeparam>
    /// <param name="result">Das Result-Objekt, das den zu validierenden Wert enthält.</param>
    /// <param name="validationFunc">Eine Funktion, die den Wert validiert und einen booleschen Wert zurückgibt.
    /// Diese Funktion sollte <c>true</c> zurückgeben, wenn der Wert gültig ist, andernfalls <c>false</c>.</param>
    /// <param name="validationError">Der Fehler, der zurückgegeben wird, wenn die Validierung fehlschlägt.</param>
    /// <returns>Ein <see cref="Result{TValue}"/>-Objekt, das entweder den ursprünglichen Wert oder einen Validierungsfehler enthält.</returns>
    public static Result<TValue> Validate<TValue>(this Result<TValue> result, Func<TValue, bool> validationFunc, Error validationError)
    {
        if (result is null || result.Value is null)
        {
            return Result<TValue>.Failure(Error.NullValue);
        }
        else if (result.IsFailure)
        {
            return result;
        }

        return validationFunc(result.Value) ? result : Result<TValue>.Failure(validationError);
    }

    /// <summary>
    /// Führt eine angegebene Funktion aus, wenn das Result erfolgreich ist.
    /// </summary>
    /// <typeparam name="TValue">Der Typ des Wertes, der im Result enthalten ist.</typeparam>
    /// <param name="result">Das Result-Objekt, das den Wert enthält, um den es geht.</param>
    /// <param name="func">Eine Funktion, die ein <see cref="Result{TValue}"/> zurückgibt.
    /// Diese Funktion wird nur aufgerufen, wenn das Result erfolgreich ist.</param>
    /// <returns>Ein <see cref="Result{TValue}"/>-Objekt, das das Ergebnis der Funktion oder das ursprüngliche Result bei einem Fehler enthält.</returns>
    public static Result<TValue> Then<TValue>(this Result<TValue> result, Func<Result<TValue>> func)
    {
        if (result.IsFailure)
        {
            return result;
        }

        return func();
    }
}
