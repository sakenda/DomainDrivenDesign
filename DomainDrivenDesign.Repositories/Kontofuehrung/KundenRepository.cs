using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesign.Persistance.Kontofuehrung
{
    public class KundeRepository
    {
        private readonly KontofuehrungDbContext _context;

        public KundeRepository(KontofuehrungDbContext context)
        {
            _context = context;
        }

        public async Task<KundeDbModel> GetKundeByIdAsync(string kundennummer)
        {
            return await _context.Kunden.FindAsync(kundennummer);
        }

        public async Task<IEnumerable<KundeDbModel>> GetAllKundenAsync()
        {
            return await _context.Kunden.ToListAsync();
        }
    }
}
