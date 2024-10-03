using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Helpers.Results;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesign.Application.Kontoeroeffnung;

[ApiController]
[Route("api/kontoeroeffnung")]
public class KontoeroeffnungController : ControllerBase
{
    private readonly KontoeroeffnungService _kontoeroeffnungService;
    private readonly ILogger<KontoeroeffnungController> _logger;

    public KontoeroeffnungController(ILogger<KontoeroeffnungController> logger, KontoeroeffnungService kontoeroeffnungService)
    {
        _logger = logger;
        _kontoeroeffnungService = kontoeroeffnungService;
    }

    [HttpGet("kunden")]
    public async Task<Result<IEnumerable<KundeDto>>> GetKunden()
    {
        var kunden = await _kontoeroeffnungService.GetAlleKunden();

        return Result<IEnumerable<KundeDto>>
            .Success(kunden)
            .WhenListHasEntries()
            .WithError(DomainErrors.Kontoeroeffnung.KundeListeNichtGeladen);
    }

    [HttpGet("girokonten")]
    public async Task<Result<IEnumerable<GirokontoDto>>> GetGirokonten()
    {
        var girokonten = await _kontoeroeffnungService.GetAlleGirokonten();

        return Result<IEnumerable<GirokontoDto>>
            .Success(girokonten)
            .WhenListHasEntries()
            .WithError(DomainErrors.Kontoeroeffnung.GirokontenListeNichtGeladen);
    }

    [HttpPost("create/kunde")]
    public async Task<Result<KundeDto>> CreateKunde(KundeDto kundeDto)
    {
        var createdKunde = await _kontoeroeffnungService.ErstelleKundeAsync(kundeDto.Vorname, kundeDto.Nachname);

        return Result<KundeDto>
            .Create(createdKunde)
            .WithError(DomainErrors.Kontoeroeffnung.KundeNichtErstellt);
    }

    [HttpPost("create/girokonto")]
    public async Task<Result<GirokontoDto>> CreateGirokonto(GirokontoDto girokontoDto)
    {
        var createdGirokonto = await _kontoeroeffnungService.ErstelleGirokontoAsync(girokontoDto.Kundennummer, girokontoDto.IBAN, girokontoDto.Kontostand);

        return Result<GirokontoDto>
            .Create(createdGirokonto)
            .WithError(DomainErrors.Kontoeroeffnung.GirokontoNichtErstellt);
    }
}
