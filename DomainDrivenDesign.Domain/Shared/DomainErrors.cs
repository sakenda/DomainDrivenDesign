using DomainDrivenDesign.Helpers.Results;
using System.Runtime.CompilerServices;

namespace DomainDrivenDesign.Domain.Shared;

public static class DomainErrors
{
    public static class Shared
    {
        public static Error CreateError(string prefix, string message, [CallerMemberName] string memberName = "")
        {
            return new Error($"{prefix}.{memberName}", message);
        }
    }

    public static class Kontoeroeffnung
    {
        private const string Prefix = nameof(Kontoeroeffnung);

        public static readonly Error KundeListeNichtGeladen = Shared.CreateError(Prefix, "Es wurden keine Kunden geladen.");
        public static readonly Error GirokontenListeNichtGeladen = Shared.CreateError(Prefix, "Es wurden keine Girokonten geladen.");
        public static readonly Error KundeNameNichtVollstaendig = Shared.CreateError(Prefix, "Der Name des Kunden ist nicht vollständig.");
        public static readonly Error KundeNichtErstellt = Shared.CreateError(Prefix, "Kunde konnte nicht erstellt werden.");
        public static readonly Error GirokontoNichtErstellt = Shared.CreateError(Prefix, "Girokonto konnte nicht erstellt werden.");

        
    }

    public static class Kontofuehrung
    {
        private const string Prefix = nameof(Kontofuehrung);

        public static readonly Error UnzureichenderKontostand = Shared.CreateError(Prefix, "Unzureichender Kontostand für die Abhebung.");
        public static readonly Error KontoNichtGefunden = Shared.CreateError(Prefix, "Das angegebene Girokonto wurde nicht gefunden.");
        public static readonly Error EinzahlungFehlgeschlagen = Shared.CreateError(Prefix, "Die Einzahlung konnte nicht durchgeführt werden.");
        public static readonly Error AbhebungFehlgeschlagen = Shared.CreateError(Prefix, "Die Abhebung konnte nicht durchgeführt werden.");
        public static readonly Error UeberweisungFehlgeschlagen = Shared.CreateError(Prefix, "Die Überweisung konnte nicht durchgeführt werden.");
        public static readonly Error KontoExistiertNicht = Shared.CreateError(Prefix, "Eines der Konten existiert nicht.");
        public static readonly Error KundeNichtGefunden = Shared.CreateError(Prefix, "Der angegebene Kunde wurde nicht gefunden.");
    }

}


