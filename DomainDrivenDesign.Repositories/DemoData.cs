using DomainDrivenDesign.Persistance.Models.Common;

namespace DomainDrivenDesign.Persistance;

public static class DemoData
{
    public static List<KundeDbModel> Kunden => new List<KundeDbModel>
    {
        new KundeDbModel("KUND001", "Max", "Mustermann"),
        new KundeDbModel("KUND002", "Anna", "Musterfrau"),
        new KundeDbModel("KUND003", "John", "Doe"),
        new KundeDbModel("KUND004", "Jane", "Doe"),
        new KundeDbModel("KUND005", "Lara", "Lustig")
    };

    public static List<GirokontoDbModel> Girokonten => new List<GirokontoDbModel>
    {
        new GirokontoDbModel("DE1234567891001", "KUND001", 1000.50m),
        new GirokontoDbModel("DE1234567891002", "KUND002", 2000.00m),
        new GirokontoDbModel("DE1234567891003", "KUND003", 1500.75m),
        new GirokontoDbModel("DE1234567891004", "KUND004", 3000.20m),
        new GirokontoDbModel("DE1234567891005", "KUND005", 500.10m)
    };
}
