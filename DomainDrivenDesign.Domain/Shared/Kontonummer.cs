using System.Numerics;

namespace DomainDrivenDesign.Domain.Shared;

public readonly record struct Kontonummer
{
    private const int MAXIMALE_NUMMERN_LAENGE = 10;

    public string IBAN { get; init; }

    public Kontonummer(string iban)
    {
        if (!IstIBANKorrekt(iban))
        {
            throw new ArgumentException("IBAN ist nicht korrekt");
        }

        IBAN = iban;
    }

    public override string ToString()
    {
        return IBAN;
    }

    private bool IstIBANKorrekt(string iban)
    {
        // 1. IBAN in Großbuchstaben und ohne Leerzeichen umwandeln
        iban = iban.ToUpper().Replace(" ", "");

        // 2. Länge der IBAN überprüfen (für Deutschland z.B. 22 Stellen)
        if (iban.Length < 15 || iban.Length > 34)
        {
            return false; // ungültige IBAN-Länge
        }

        // 3. Den Ländercode und die Prüfziffer an das Ende der IBAN setzen
        string rearrangedIban = iban.Substring(4) + iban.Substring(0, 4);

        // 4. Buchstaben in Zahlen umwandeln (A = 10, B = 11, ..., Z = 35)
        string numericIban = "";
        foreach (char c in rearrangedIban)
        {
            if (char.IsLetter(c))
            {
                numericIban += (c - 'A' + 10).ToString();
            }
            else
            {
                numericIban += c;
            }
        }

        // 5. Modulo 97 berechnen und prüfen, ob der Rest 1 ist
        BigInteger ibanAsNumber = BigInteger.Parse(numericIban);
        return ibanAsNumber % 97 == 1;
    }

}
