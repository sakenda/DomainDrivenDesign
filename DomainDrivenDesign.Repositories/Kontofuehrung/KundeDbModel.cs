using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.Persistance.Kontofuehrung;

public record KundeDbModel
{
    [Key]
    public string Kundennummer { get; init; }

    [Required]
    [MaxLength(100)]
    public string Vorname { get; init; }

    [Required]
    [MaxLength(100)]
    public string Nachname { get; init; }

    public KundeDbModel(string kundennummer, string vorname, string nachname)
    {
        Kundennummer = kundennummer;
        Vorname = vorname;
        Nachname = nachname;
    }

}
