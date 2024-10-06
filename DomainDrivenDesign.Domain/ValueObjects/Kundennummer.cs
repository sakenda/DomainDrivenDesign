namespace DomainDrivenDesign.Domain.ValueObjects;

public readonly record struct Kundennummer
{
    private const int KUNDENNUMMER_LAENGE = 5;

    public string Nummer { get; }

    public Kundennummer(string nummer)
    {
        if (string.IsNullOrWhiteSpace(nummer))
        {
            throw new ArgumentException("Die Kundennummer darf nicht leer sein.", nameof(nummer));
        }

        if (nummer.Length != KUNDENNUMMER_LAENGE)
        {
            throw new ArgumentException("Die Kundennummer muss 5 Zeichen lang sein.", nameof(nummer));
        }

        Nummer = nummer;
    }

    public override string ToString() => Nummer;
}
