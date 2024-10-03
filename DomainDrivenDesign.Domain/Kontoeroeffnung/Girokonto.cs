
using DomainDrivenDesign.Domain.Shared;

namespace DomainDrivenDesign.Domain.Kontoeroeffnung;

public record Girokonto
{
    public Kunde Kunde { get; init; }
    public Kontonummer Kontonummer { get; init; }
    public Betrag Kontostand { get; private set; }

    public Girokonto(Kunde kunde, Kontonummer kontonummer, Betrag startBetrag)
    {
        Kunde = kunde ?? throw new ArgumentNullException(nameof(kunde));
        Kontonummer = kontonummer;
        Kontostand = startBetrag;
    }

    public void InitialeEinzahlung(Betrag betrag)
    {
        if (Kontostand.Wert > 0)
        {
            throw new InvalidOperationException("Eine initiale Einzahlung ist nur möglich, wenn das Konto leer ist.");
        }

        Kontostand = Kontostand.Addiere(betrag);
    }

    public override string ToString()
    {
        return $"Girokonto {Kontonummer} von {Kunde}, Kontostand: {Kontostand}";
    }

}
