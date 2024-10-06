namespace DomainDrivenDesign.Helpers.ValueObjects;

/// <summary>
/// Abstrakte Basisklasse für Value Objects.
/// Value Objects sind Objekte, die durch ihre Attribute definiert sind und keine Identität besitzen.
/// Zwei Value Objects gelten als gleich, wenn alle ihre Attribute gleich sind.
/// </summary>
/// <typeparam name="T">Der Typ, der von ValueObject erbt. Dieser muss ein ValueObject sein.</typeparam>
public abstract record ValueObject<T> where T : ValueObject<T>
{
    /// <summary>
    /// Überschreibt die GetHashCode-Methode, um einen Hash-Code basierend auf den Gleichheitskomponenten zu generieren.
    /// </summary>
    /// <returns>Ein Hash-Code für das aktuelle Value Object.</returns>
    public override int GetHashCode()
    {
        // Generiere einen Hash-Code durch Kombination der Hash-Codes der Gleichheitskomponenten.
        return GetEqualityComponents()
            .Aggregate(1, (current, obj) => current * 31 + (obj?.GetHashCode() ?? 0));
    }

    /// <summary>
    /// Diese Methode muss in den abgeleiteten Klassen implementiert werden.
    /// Sie gibt die Komponenten zurück, die zur Bestimmung der Gleichheit verwendet werden.
    /// </summary>
    /// <returns>Eine IEnumerable von Objekten, die die Gleichheitskomponenten darstellen.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();
}
