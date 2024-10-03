using System.ComponentModel.DataAnnotations;

namespace DomainDrivenDesign.WebApp.Models.Kontoeroeffnung;

public class Girokonto
{
    [Required(ErrorMessage = "Ein Konto kann nur mit einem Kunde erstellt werden.")]
    public string Kundennummer { get; set; }

    [Required(ErrorMessage = "Es muss zwingend eine IBAN angegeben werden.")]
    public string IBAN { get; set; }

    [Range(0.01d, double.MaxValue, ErrorMessage = "Der Kontostand muss ein Guthaben aufweisen.")]
    public decimal Kontostand { get; set; }
}
