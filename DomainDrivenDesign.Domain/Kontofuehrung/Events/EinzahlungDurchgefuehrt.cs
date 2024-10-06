using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ValueObjects;

namespace DomainDrivenDesign.Persistance.Kontofuehrung.Events;

public class EinzahlungDurchgefuehrt
{
    public Betrag Betrag { get; }
    public Kontonummer Kontonummer { get; }
    public DateTime Zeitpunkt { get; }

    public EinzahlungDurchgefuehrt(Betrag betrag, Kontonummer kontonummer)
    {
        Betrag = betrag;
        Kontonummer = kontonummer;
        Zeitpunkt = DateTime.UtcNow;
    }
}
