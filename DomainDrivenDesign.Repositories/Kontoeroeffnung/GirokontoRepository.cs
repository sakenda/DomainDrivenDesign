using DomainDrivenDesign.Persistance.Database;
using DomainDrivenDesign.Persistance.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistence.Kontoeroeffnung;

public class GirokontoRepository
{
    private readonly BankDbContext _context;

    public GirokontoRepository(BankDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GirokontoDbModel> GetGirokontoByIBANAsync(string iban)
    {
        return await _context.Girokonten.FindAsync(iban);
    }

    public async Task<IEnumerable<GirokontoDbModel>> GetAllGirokontenAsync()
    {
        return await _context.Girokonten.ToListAsync();
    }

    public async Task<IEnumerable<GirokontoDbModel>> GetGirokontenByKundennummerAsync(string kundennummer)
    {
        return await _context.Girokonten
            .Where(g => g.Kundennummer == kundennummer)
            .ToListAsync();
    }

    public async Task AddGirokontoAsync(GirokontoDbModel girokonto)
    {
        await _context.Girokonten.AddAsync(girokonto);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGirokontoAsync(GirokontoDbModel girokonto)
    {
        _context.Girokonten.Update(girokonto);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteGirokontoAsync(string iban)
    {
        var girokonto = await GetGirokontoByIBANAsync(iban);
        if (girokonto != null)
        {
            _context.Girokonten.Remove(girokonto);
            await _context.SaveChangesAsync();
        }
    }
}

