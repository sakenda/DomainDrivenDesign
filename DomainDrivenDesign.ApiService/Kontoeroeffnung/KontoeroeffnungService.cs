using DomainDrivenDesign.Persistance.Models.Common;
using DomainDrivenDesign.Persistence.Kontoeroeffnung;

namespace DomainDrivenDesign.Application.Kontoeroeffnung;

public class KontoeroeffnungService
{
    private readonly KundenRepository _kundenRepository;
    private readonly GirokontoRepository _girokontoRepository;

    private readonly GirokontoDbToGirokontoDtoMapper _girokontoMapper = new GirokontoDbToGirokontoDtoMapper();
    private readonly KundeDbToKundeDtoMapper _kundeMapper = new KundeDbToKundeDtoMapper();

    public KontoeroeffnungService(KundenRepository kundenRepository, GirokontoRepository girokontoRepository)
    {
        _kundenRepository = kundenRepository ?? throw new ArgumentNullException(nameof(kundenRepository));
        _girokontoRepository = girokontoRepository ?? throw new ArgumentNullException(nameof(girokontoRepository));
    }

    public async Task<IEnumerable<KundeDto>> GetAlleKunden()
    {
        var kunden = await _kundenRepository.GetAlleKundenAsync();
        var result = kunden.Select(_kundeMapper.MapToOutput);

        return result;
    }

    public async Task<IEnumerable<GirokontoDto>> GetAlleGirokonten()
    {
        var konten = await _girokontoRepository.GetAllGirokontenAsync();
        var result = konten.Select(_girokontoMapper.MapToOutput);

        return result;
    }

    public async Task<KundeDto> ErstelleKundeAsync(string vorname, string nachname)
    {
        var kundennummer = Guid.NewGuid().ToString().Substring(0, 5);
        var kunde = new KundeDbModel(kundennummer, vorname, nachname);

        await _kundenRepository.AddKundeAsync(kunde);

        return _kundeMapper.MapToOutput(kunde);
    }

    public async Task<GirokontoDto> ErstelleGirokontoAsync(string kundennummer, string iban, decimal anfänglicherKontostand)
    {
        var kunde = await _kundenRepository.GetKundeByKundennummerAsync(kundennummer);

        if (kunde == null)
        {
            throw new InvalidOperationException($"Kunde mit Kundennummer {kundennummer} existiert nicht.");
        }

        var girokonto = new GirokontoDbModel(iban, kundennummer, anfänglicherKontostand);

        await _girokontoRepository.AddGirokontoAsync(girokonto);

        return _girokontoMapper.MapToOutput(girokonto);
    }
}
