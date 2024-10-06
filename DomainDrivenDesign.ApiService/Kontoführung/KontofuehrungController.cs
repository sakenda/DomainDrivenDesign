using DomainDrivenDesign.Helpers.Results;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenDesign.Application.Kontoführung;

[Route("api/kontofuehrung")]
[ApiController]
public class KontofuehrungController : ControllerBase
{
    private readonly KontofuehrungService _kontofuehrungService;

    public KontofuehrungController(KontofuehrungService kontofuehrungService)
    {
        _kontofuehrungService = kontofuehrungService;
    }

    [HttpPost("einzahlen/{iban}")]
    public async Task<Result<decimal>> Einzahlen(string iban, [FromBody] decimal betrag)
    {
        return await _kontofuehrungService.EinzahlenAsync(iban, betrag);
    }

    [HttpPost("abheben/{iban}")]
    public async Task<Result<decimal>> Abheben(string iban, [FromBody] decimal betrag)
    {
        return await _kontofuehrungService.AbhebenAsync(iban, betrag);
    }

    [HttpPost("ueberweisen")]
    public async Task<Result<decimal>> Ueberweisen([FromBody] UeberweisungRequest request)
    {
        return await _kontofuehrungService.UeberweisenAsync(request.VonIban, request.NachIban, request.Betrag);
    }
}

public class UeberweisungRequest
{
    public string VonIban { get; set; }
    public string NachIban { get; set; }
    public decimal Betrag { get; set; }
}
