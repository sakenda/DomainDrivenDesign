using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.Persistance.Kontofuehrung;

public record GirokontoDbModel
{
    [Key]
    public string IBAN { get; init; }

    [Required]
    public string Kundennummer { get; init; }

    [Required]
    public decimal Kontostand { get; set; }

    public GirokontoDbModel(string iBAN, string kundennummer, decimal kontostand)
    {
        IBAN = iBAN;
        Kundennummer = kundennummer;
        Kontostand = kontostand;
    }
}
