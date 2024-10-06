using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ValueObjects;

namespace DomainDrivenDesign.Domain.Kontofuehrung.Events;

public class AbhebungDurchgefuehrt
{
    public Betrag Betrag { get; }
    public Kontonummer Kontonummer { get; }
    public DateTime Zeitpunkt { get; }

    public AbhebungDurchgefuehrt(Betrag betrag, Kontonummer kontonummer)
    {
        Betrag = betrag;
        Kontonummer = kontonummer;
        Zeitpunkt = DateTime.UtcNow;
    }
}