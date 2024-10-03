
namespace DomainDrivenDesign.Helpers.Results;

/// <summary>
/// Repräsentiert einen Fehler, der mit einem Code und einer Nachricht beschrieben wird.
/// Diese Klasse implementiert <see cref="IEquatable{Error}"/>, um Fehlerobjekte miteinander vergleichen zu können.
/// </summary>
public class Error : IEquatable<Error>
{
    /// <summary>
    /// Ein statisches Error-Objekt, das keinen Fehler repräsentiert (leere Zeichenfolgen).
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);

    /// <summary>
    /// Ein statisches Error-Objekt, das angibt, dass ein Nullwert verwendet wurde, wo dies nicht erlaubt war.
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "Das ergebnis ist NULL.");

    /// <summary>
    /// Ein Fehler, der auftritt, wenn eine Liste keine Einträge enthält.
    /// </summary>
    /// <remarks>
    /// Diese Fehlerkonstante kann verwendet werden, um klarzustellen, dass eine
    /// erwartete Liste keine Elemente enthält, was für die Logik der Anwendung
    /// relevant sein kann.
    /// </remarks>
    public static readonly Error ListHasNoEntries = new("Error.NoEntries", "Die Liste hat keine Einträge.");


    /// <summary>
    /// Initialisiert eine neue Instanz der <see cref="Error"/>-Klasse mit einem spezifischen Fehlercode und einer Fehlermeldung.
    /// </summary>
    /// <param name="code">Der eindeutige Fehlercode, der den Fehler beschreibt.</param>
    /// <param name="message">Die Fehlermeldung, die den Fehler beschreibt.</param>
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Der Fehlercode, der den Fehler eindeutig beschreibt.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Die Fehlermeldung, die den Fehler beschreibt.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Ermöglicht eine implizite Konvertierung eines <see cref="Error"/>-Objekts zu einem String, wobei der Fehlercode zurückgegeben wird.
    /// </summary>
    /// <param name="error">Das <see cref="Error"/>-Objekt, das in einen String konvertiert wird.</param>
    public static implicit operator string(Error error) => error.Code;

    /// <summary>
    /// Vergleicht zwei <see cref="Error"/>-Objekte auf Gleichheit.
    /// </summary>
    /// <param name="a">Das erste <see cref="Error"/>-Objekt.</param>
    /// <param name="b">Das zweite <see cref="Error"/>-Objekt.</param>
    /// <returns><c>true</c>, wenn die beiden Fehler gleich sind, andernfalls <c>false</c>.</returns>
    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    /// <summary>
    /// Vergleicht zwei <see cref="Error"/>-Objekte auf Ungleichheit.
    /// </summary>
    /// <param name="a">Das erste <see cref="Error"/>-Objekt.</param>
    /// <param name="b">Das zweite <see cref="Error"/>-Objekt.</param>
    /// <returns><c>true</c>, wenn die beiden Fehler ungleich sind, andernfalls <c>false</c>.</returns>
    public static bool operator !=(Error? a, Error? b) => !(a == b);

    /// <summary>
    /// Implementiert die Logik, um zwei <see cref="Error"/>-Objekte auf Gleichheit zu überprüfen.
    /// </summary>
    /// <param name="other">Das andere <see cref="Error"/>-Objekt, das verglichen wird.</param>
    /// <returns><c>true</c>, wenn die Fehler gleich sind, andernfalls <c>false</c>.</returns>
    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    /// <summary>
    /// Vergleicht das aktuelle Objekt mit einem anderen Objekt und überprüft, ob beide gleich sind.
    /// </summary>
    /// <param name="obj">Das Objekt, mit dem verglichen wird.</param>
    /// <returns><c>true</c>, wenn das Objekt ein <see cref="Error"/> ist und gleich ist, andernfalls <c>false</c>.</returns>
    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    /// <summary>
    /// Berechnet einen Hashcode basierend auf dem Fehlercode und der Fehlermeldung.
    /// </summary>
    /// <returns>Der Hashcode des <see cref="Error"/>-Objekts.</returns>
    public override int GetHashCode() => HashCode.Combine(Code, Message);

    /// <summary>
    /// Gibt die Fehlermeldung als Zeichenfolge zurück.
    /// </summary>
    /// <returns>Die Fehlermeldung des <see cref="Error"/>-Objekts.</returns>
    public override string ToString() => Message;
}

