using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistance.Kontofuehrung;

public class KontofuehrungDbContext : DbContext
{
    public DbSet<GirokontoDbModel> Girokonten { get; set; }
    public DbSet<KundeDbModel> Kunden { get; set; }

    public KontofuehrungDbContext(DbContextOptions<KontofuehrungDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KundeDbModel>()
            .HasKey(k => k.Kundennummer);

        modelBuilder.Entity<GirokontoDbModel>()
            .HasKey(g => g.IBAN); // Die Primärschlüsseldefinition wurde hier korrigiert

        modelBuilder.Entity<GirokontoDbModel>()
            .HasOne<KundeDbModel>()
            .WithMany() // Dies ist implicit, da es keine Navigation zurück zu Kunde gibt
            .HasForeignKey(g => g.Kundennummer);

        var kunden = new List<KundeDbModel>()
        {
            new KundeDbModel(Guid.NewGuid().ToString().Substring(0, 5), "Max", "Mustermann"),
            new KundeDbModel(Guid.NewGuid().ToString().Substring(0, 5), "Anna", "Musterfrau"),
            new KundeDbModel(Guid.NewGuid().ToString().Substring(0, 5), "John", "Doe"),
            new KundeDbModel(Guid.NewGuid().ToString().Substring(0, 5), "Jane", "Doe"),
            new KundeDbModel(Guid.NewGuid().ToString().Substring(0, 5), "Lara", "Lustig")
        };

        var girokonten = new List<GirokontoDbModel>()
        {
            new GirokontoDbModel("DE1234567891001", kunden[0].Kundennummer, 1000.50m),
            new GirokontoDbModel("DE1234567891002", kunden[1].Kundennummer, 2000.00m),
            new GirokontoDbModel("DE1234567891003", kunden[2].Kundennummer, 1500.75m),
            new GirokontoDbModel("DE1234567891004", kunden[3].Kundennummer, 3000.20m),
            new GirokontoDbModel("DE1234567891005", kunden[4].Kundennummer, 500.10m)
        };

        // Seed - Daten hinzufügen
        modelBuilder.Entity<KundeDbModel>().HasData(kunden);
        modelBuilder.Entity<GirokontoDbModel>().HasData(girokonten);
    }
}
