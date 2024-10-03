using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.WebApp.Models.Kontoeroeffnung;

public class Kunde
{
    public string Kundennummer { get; set; } = "";

    [Required(ErrorMessage = "Vorname fehlt.")]
    public string Vorname { get; set; }

    [Required(ErrorMessage = "Nachname fehlt.")]
    public string Nachname { get; set; }
}
