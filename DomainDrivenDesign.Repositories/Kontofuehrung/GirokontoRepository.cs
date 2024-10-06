using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistance.Kontofuehrung
{
    public class GirokontoRepository
    {
        private readonly KontofuehrungDbContext _context;

        public GirokontoRepository(KontofuehrungDbContext context)
        {
            _context = context;
        }

        public async Task<GirokontoDbModel> GetGirokontoByIbanAsync(string iban)
        {
            return await _context.Girokonten.FindAsync(iban);
        }

        public async Task<IEnumerable<GirokontoDbModel>> GetAllGirokontenAsync()
        {
            return await _context.Girokonten.ToListAsync();
        }

        public async Task EinzahlenAsync(string iban, decimal betrag)
        {
            var girokonto = await GetGirokontoByIbanAsync(iban);
            if (girokonto != null)
            {
                girokonto.Kontostand += betrag;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AbhebenAsync(string iban, decimal betrag)
        {
            var girokonto = await GetGirokontoByIbanAsync(iban);
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
                await AbhebenAsync(vonIban, betrag);
                await EinzahlenAsync(nachIban, betrag);
            }
            else
            {
                throw new InvalidOperationException("Eines der Konten existiert nicht.");
            }
        }
    }
}
