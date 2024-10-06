using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Helpers.Results;
using DomainDrivenDesign.Persistance.Kontofuehrung;

namespace DomainDrivenDesign.Application.Kontoführung;

public class KontofuehrungService
{
    private readonly GirokontoRepository _girokontoRepository;
    private readonly KundeRepository _kundeRepository;

    public KontofuehrungService(GirokontoRepository girokontoRepository, KundeRepository kundeRepository)
    {
        _girokontoRepository = girokontoRepository;
        _kundeRepository = kundeRepository;
    }

    public async Task<Result<decimal>> EinzahlenAsync(string iban, decimal betrag)
    {
        try
        {
            await _girokontoRepository.EinzahlenAsync(iban, betrag);
            var girokonto = await _girokontoRepository.GetGirokontoByIbanAsync(iban);
            return Result<decimal>.Success(girokonto.Kontostand);
        }
        catch (Exception)
        {
            return Result<decimal>.Failure(DomainErrors.Kontofuehrung.EinzahlungFehlgeschlagen);
        }
    }

    public async Task<Result<decimal>> AbhebenAsync(string iban, decimal betrag)
    {
        try
        {
            await _girokontoRepository.AbhebenAsync(iban, betrag);
            var girokonto = await _girokontoRepository.GetGirokontoByIbanAsync(iban);
            return Result<decimal>.Success(girokonto.Kontostand);
        }
        catch (InvalidOperationException)
        {
            return Result<decimal>.Failure(DomainErrors.Kontofuehrung.UnzureichenderKontostand);
        }
        catch (Exception)
        {
            return Result<decimal>.Failure(DomainErrors.Kontofuehrung.AbhebungFehlgeschlagen);
        }
    }

    public async Task<Result<decimal>> UeberweisenAsync(string vonIban, string nachIban, decimal betrag)
    {
        try
        {
            await _girokontoRepository.UeberweisenAsync(vonIban, nachIban, betrag);
            var vonGirokonto = await _girokontoRepository.GetGirokontoByIbanAsync(vonIban);
            return Result<decimal>.Success(vonGirokonto.Kontostand);
        }
        catch (InvalidOperationException)
        {
            return Result<decimal>.Failure(DomainErrors.Kontofuehrung.KontoNichtGefunden);
        }
        catch (Exception)
        {
            return Result<decimal>.Failure(DomainErrors.Kontofuehrung.UeberweisungFehlgeschlagen);
        }
    }
}
