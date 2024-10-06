using DomainDrivenDesign.Domain.Shared;

namespace DomainDrivenDesign.Domain.ValueObjects;

public readonly record struct Betrag
{
    public decimal Wert { get; }
    public Waehrungen Waehrung { get; }

    public Betrag(decimal wert, Waehrungen währung)
    {
        if (wert < 0)
        {
            throw new ArgumentException("Der Betrag darf nicht negativ sein.");
        }

        Wert = wert;
        Waehrung = währung;
    }

    public Betrag Addiere(Betrag andererBetrag)
    {
        if (Waehrung != andererBetrag.Waehrung)
        {
            throw new InvalidOperationException("Währungen müssen übereinstimmen.");
        }

        return new Betrag(Wert + andererBetrag.Wert, Waehrung);
    }

    public override string ToString()
    {
        return $"{Wert} {Waehrung}";
    }
}
