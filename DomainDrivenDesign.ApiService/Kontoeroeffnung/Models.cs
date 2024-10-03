
namespace DomainDrivenDesign.Application.Kontoeroeffnung;

public record KundeDto(string Kundennummer, string Vorname, string Nachname);
public record GirokontoDto(string IBAN, string Kundennummer, decimal Kontostand);
