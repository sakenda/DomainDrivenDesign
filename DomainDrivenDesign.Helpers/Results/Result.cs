using System.Text.Json.Serialization;

namespace DomainDrivenDesign.Helpers.Results;

/// <summary>
/// Repräsentiert das Ergebnis eines Vorgangs, bei dem entweder Erfolg oder Misserfolg eintreten kann.
/// </summary>
public class Result
{
    /// <summary>
    /// Gibt an, ob der Vorgang erfolgreich war.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gibt an, ob der Vorgang fehlgeschlagen ist (das Gegenteil von IsSuccess).
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Der Fehler, der bei einem fehlgeschlagenen Vorgang aufgetreten ist.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Privater Konstruktor, der verwendet wird, um ein Result-Objekt zu erstellen.
    /// Kann nur durch die statischen Methoden <see cref="Success"/> oder <see cref="Failure"/> instanziiert werden.
    /// </summary>
    /// <param name="isSuccess">Gibt an, ob der Vorgang erfolgreich war.</param>
    /// <param name="error">Der aufgetretene Fehler, wenn der Vorgang fehlschlug.</param>
    [JsonConstructor]
    private Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Erstellt ein erfolgreiches <see cref="Result"/>-Objekt.
    /// </summary>
    /// <returns>Ein erfolgreiches <see cref="Result"/>-Objekt ohne Fehler.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Erstellt ein fehlgeschlagenes <see cref="Result"/>-Objekt.
    /// </summary>
    /// <param name="error">Der Fehler, der den Misserfolg beschreibt.</param>
    /// <returns>Ein fehlgeschlagenes <see cref="Result"/>-Objekt mit einem Fehler.</returns>
    public static Result Failure(Error error) => new(false, error);
}

/// <summary>
/// Generische Variante des <see cref="Result"/>, die zusätzlich ein Wert-Objekt enthält, das bei Erfolg zurückgegeben wird.
/// </summary>
/// <typeparam name="TValue">Der Typ des Werts, der bei einem erfolgreichen Vorgang zurückgegeben wird.</typeparam>
public class Result<TValue>
{
    /// <summary>
    /// Der Wert, der bei einem erfolgreichen Vorgang zurückgegeben wird.
    /// </summary>
    private readonly TValue? _value;

    /// <summary>
    /// Gibt an, ob der Vorgang erfolgreich war.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gibt an, ob der Vorgang fehlgeschlagen ist (das Gegenteil von IsSuccess).
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Der Fehler, der bei einem fehlgeschlagenen Vorgang aufgetreten ist.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Der Wert, der bei einem erfolgreichen Vorgang zurückgegeben wird. Wenn der Vorgang fehlschlägt, ist der Wert <c>default</c>.
    /// </summary>
    public TValue? Value => IsSuccess ? _value : default;

    /// <summary>
    /// Privater Konstruktor, der verwendet wird, um ein generisches Result-Objekt zu erstellen.
    /// Kann nur durch die statischen Methoden <see cref="Success(TValue)"/> oder <see cref="Failure(Error)"/> instanziiert werden.
    /// </summary>
    /// <param name="value">Der Wert des erfolgreichen Ergebnisses.</param>
    /// <param name="isSuccess">Gibt an, ob der Vorgang erfolgreich war.</param>
    /// <param name="error">Der Fehler, der den Misserfolg beschreibt, falls der Vorgang fehlgeschlagen ist.</param>
    [JsonConstructor]
    private Result(TValue? value, bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        _value = value;
        Error = error;
    }

    /// <summary>
    /// Erstellt ein erfolgreiches <see cref="Result{TValue}"/>-Objekt mit einem Wert.
    /// </summary>
    /// <param name="value">Der Wert des erfolgreichen Ergebnisses.</param>
    /// <returns>Ein erfolgreiches <see cref="Result{TValue}"/>-Objekt mit einem Wert.</returns>
    public static Result<TValue> Success(TValue value) => new(value, true, Error.None);

    /// <summary>
    /// Erstellt ein fehlgeschlagenes <see cref="Result{TValue}"/>-Objekt mit einem Fehler.
    /// </summary>
    /// <param name="error">Der Fehler, der den Misserfolg beschreibt.</param>
    /// <returns>Ein fehlgeschlagenes <see cref="Result{TValue}"/>-Objekt.</returns>
    public static Result<TValue> Failure(Error error) => new(default, false, error);

    /// <summary>
    /// Erstellt ein <see cref="Result{TValue}"/>-Objekt basierend auf dem Wert.
    /// Wenn der Wert nicht null ist, wird ein Erfolg zurückgegeben, ansonsten ein Misserfolg.
    /// </summary>
    /// <param name="value">Der Wert des Erfolgs oder <c>null</c>.</param>
    /// <returns>Ein <see cref="Result{TValue}"/>-Objekt, das entweder erfolgreich oder fehlgeschlagen ist.</returns>
    public static Result<TValue> Create(TValue? value)
    {
        return value is not null ? Success(value) : Failure(Error.NullValue);
    }

}
