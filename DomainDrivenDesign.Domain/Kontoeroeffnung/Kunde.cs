
namespace DomainDrivenDesign.Domain.Kontoeroeffnung;

public record Kunde
{
    public Kundennummer Kundennummer { get; init; }
    public string Vorname { get; init; }
    public string Nachname { get; init; }

    public Kunde(Kundennummer kundennummer, string vorname, string nachname)
    {
        Kundennummer = kundennummer;
        ArgumentNullException.ThrowIfNullOrWhiteSpace(vorname, nameof(vorname));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(nachname, nameof(nachname));

        Vorname = vorname;
        Nachname = nachname;
    }

    public override string ToString() => $"{Vorname} {Nachname} (Kundennummer: {Kundennummer})";

}
