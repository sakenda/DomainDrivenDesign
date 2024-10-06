namespace DomainDrivenDesign.Application.Kontoführung;

public record UeberweisungRequest(string VonIban, string NachIban, decimal Betrag);