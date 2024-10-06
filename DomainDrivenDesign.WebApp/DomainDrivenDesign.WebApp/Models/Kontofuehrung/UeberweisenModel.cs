using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.WebApp.Models.Kontofuehrung;

public class UeberweisenModel
{
    [Required(ErrorMessage = "Von IBAN ist erforderlich.")]
    public string VonIban { get; set; }

    [Required(ErrorMessage = "Nach IBAN ist erforderlich.")]
    public string NachIban { get; set; }

    [Required(ErrorMessage = "Betrag ist erforderlich.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Der Betrag muss größer als 0 sein.")]
    public decimal Betrag { get; set; }
}
