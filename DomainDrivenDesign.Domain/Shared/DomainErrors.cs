using DomainDrivenDesign.Helpers.Results;
using System.Runtime.CompilerServices;

namespace DomainDrivenDesign.Domain.Shared;

public static class DomainErrors
{
    public static class Kontoeroeffnung
    {
        private const string Prefix = nameof(Kontoeroeffnung);

        public static readonly Error KundeListeNichtGeladen = CreateError("Es wurden keine Kunden geladen.");
        public static readonly Error GirokontenListeNichtGeladen = CreateError("Es wurden keine Girokonten geladen.");

        public static readonly Error KundeNameNichtVollstaendig = CreateError("Der Name des Kunden ist nicht vollständig.");
        public static readonly Error KundeNichtErstellt = CreateError("Kunde konnte nicht erstellt werden.");
        public static readonly Error GirokontoNichtErstellt = CreateError("Girokonto konnte nicht erstellt werden.");

        private static Error CreateError(string message, [CallerMemberName] string memberName = "")
        {
            return new Error($"{Prefix}.{memberName}", message);
        }
    }
}


