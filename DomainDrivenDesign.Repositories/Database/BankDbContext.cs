using DomainDrivenDesign.Persistance.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistance.Database;

public class BankDbContext : DbContext
{
    public DbSet<KundeDbModel> Kunden { get; set; }
    public DbSet<GirokontoDbModel> Girokonten { get; set; }

    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KundeDbModel>()
            .HasKey(k => k.Kundennummer);

        modelBuilder.Entity<GirokontoDbModel>()
            .HasKey(g => g.IBAN);

        modelBuilder.Entity<GirokontoDbModel>()
            .HasOne<KundeDbModel>()
            .WithMany()
            .HasForeignKey(g => g.Kundennummer);

        // Hier kannst du die Demodaten hinzufügen
        modelBuilder.Entity<KundeDbModel>().HasData(DemoData.Kunden);
        modelBuilder.Entity<GirokontoDbModel>().HasData(DemoData.Girokonten);
    }
}