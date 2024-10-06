using DomainDrivenDesign.Persistance.Database;
using DomainDrivenDesign.Persistance.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistance.Kontofuehrung
{
    public class GirokontoRepository
    {
        private readonly BankDbContext _context;

        public GirokontoRepository(BankDbContext context)
        {
            _context = context;
        }

        public async Task<GirokontoDbModel> GetGirokontoByIbanAsync(string iban)
        {
            var test = _context.Girokonten.ToList();
            return await _context.Girokonten.FindAsync(iban);
        }

        public async Task<IEnumerable<GirokontoDbModel>> GetAllGirokontenAsync()
        {
            return await _context.Girokonten.ToListAsync();
        }

        public async Task EinzahlenAsync(GirokontoDbModel girokonto, decimal betrag)
        {
            if (girokonto != null)
            {
                girokonto.Kontostand += betrag;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AbhebenAsync(GirokontoDbModel girokonto, decimal betrag)
        {
            if (girokonto != null && girokonto.Kontostand >= betrag)
            {
                girokonto.Kontostand -= betrag;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Unzureichender Kontostand für die Abhebung.");
            }
        }

        public async Task UeberweisenAsync(string vonIban, string nachIban, decimal betrag)
        {
            var vonGirokonto = await GetGirokontoByIbanAsync(vonIban);
            var nachGirokonto = await GetGirokontoByIbanAsync(nachIban);

            if (vonGirokonto != null && nachGirokonto != null)
            {
                await AbhebenAsync(vonGirokonto, betrag);
                await EinzahlenAsync(nachGirokonto, betrag);
            }
            else
            {
                throw new InvalidOperationException("Eines der Konten existiert nicht.");
            }
        }
    }
}
