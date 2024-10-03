using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.Persistence.Kontoeroeffnung;

public record GirokontoDbModel
{
    [Key]
    public string IBAN { get; init; }

    [Required]
    public string Kundennummer { get; init; }

    [Required]
    public decimal Kontostand { get; init; }

    public GirokontoDbModel(string iBAN, string kundennummer, decimal kontostand)
    {
        IBAN = iBAN;
        Kundennummer = kundennummer;
        Kontostand = kontostand;
    }

}
