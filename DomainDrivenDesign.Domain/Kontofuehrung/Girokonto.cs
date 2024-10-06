using DomainDrivenDesign.Domain.Kontofuehrung.Events;
using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ValueObjects;
using DomainDrivenDesign.Helpers.ValueObjects;
using DomainDrivenDesign.Persistance.Kontofuehrung.Events;

namespace DomainDrivenDesign.Domain.Kontofuehrung;

public record Girokonto : ValueObject<Girokonto>
{
    public event EventHandler<EinzahlungDurchgefuehrt>? EinzahlungDurchgefuehrt;
    public event EventHandler<AbhebungDurchgefuehrt>? AbhebungDurchgefuehrt;

    public Kunde Kunde { get; init; }
    public Kontonummer Kontonummer { get; init; }
    public Betrag Kontostand { get; private set; }

    public Girokonto(Kunde kunde, Kontonummer kontonummer, Betrag startBetrag)
    {
        Kunde = kunde ?? throw new ArgumentNullException(nameof(kunde));
        Kontonummer = kontonummer;
        Kontostand = startBetrag;
    }

    public void Einzahlen(Betrag betrag)
    {
        if (betrag.Wert <= 0)
        {
            throw new ArgumentException("Der Betrag muss größer als null sein.", nameof(betrag));
        }

        Kontostand = new Betrag(Kontostand.Wert + betrag.Wert, Kontostand.Waehrung);
        EinzahlungDurchgefuehrt?.Invoke(this, new EinzahlungDurchgefuehrt(betrag, Kontonummer));
    }

    public void Abheben(Betrag betrag)
    {
        if (betrag.Wert <= 0)
        {
            throw new ArgumentException("Der Betrag muss größer als null sein.", nameof(betrag));
        }

        if (Kontostand.Wert < betrag.Wert)
        {
            throw new InvalidOperationException("Unzureichender Kontostand für diese Abhebung.");
        }

        Kontostand = new Betrag(Kontostand.Wert - betrag.Wert, Kontostand.Waehrung);
        AbhebungDurchgefuehrt?.Invoke(this, new AbhebungDurchgefuehrt(betrag, Kontonummer));
    }

    public override string ToString()
    {
        return $"Girokonto {Kontonummer} von {Kunde}, Kontostand: {Kontostand}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Kunde;
        yield return Kontonummer;
    }
}
