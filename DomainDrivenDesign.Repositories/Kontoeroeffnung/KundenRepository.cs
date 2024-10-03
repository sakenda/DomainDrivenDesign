using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistence.Kontoeroeffnung;

public class KundenRepository
{
    private readonly KontoeroeffnungDbContext _context;

    public KundenRepository(KontoeroeffnungDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<KundeDbModel> GetKundeByKundennummerAsync(string kundennummer)
    {
        return await _context.Kunden.FindAsync(kundennummer);
    }

    public async Task<IEnumerable<KundeDbModel>> GetAlleKundenAsync()
    {
        return await _context.Kunden.ToListAsync();
    }

    public async Task AddKundeAsync(KundeDbModel kunde)
    {
        await _context.Kunden.AddAsync(kunde);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateKundeAsync(KundeDbModel kunde)
    {
        _context.Kunden.Update(kunde);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteKundeAsync(string kundennummer)
    {
        var kunde = await GetKundeByKundennummerAsync(kundennummer);
        if (kunde != null)
        {
            _context.Kunden.Remove(kunde);
            await _context.SaveChangesAsync();
        }
    }
}

