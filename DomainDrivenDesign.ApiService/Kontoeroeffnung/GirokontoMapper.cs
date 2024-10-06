using DomainDrivenDesign.Domain.Kontoeroeffnung;
using DomainDrivenDesign.Domain.Shared;
using DomainDrivenDesign.Domain.ValueObjects;
using DomainDrivenDesign.Helpers.Mappers;
using DomainDrivenDesign.Persistance.Models.Common;
using DomainDrivenDesign.Persistence.Kontoeroeffnung;

namespace DomainDrivenDesign.Application.Kontoeroeffnung;

public class GirokontoDtoToGirokontoMapper : BaseMapper<GirokontoDto, Girokonto>
{
    protected override Girokonto DefaultMapToOutput(GirokontoDto girokontoDto)
    {
        return new Girokonto(
            new Kunde(new Kundennummer(girokontoDto.Kundennummer), "", ""),
            new Kontonummer(girokontoDto.IBAN),
            new Betrag(girokontoDto.Kontostand, Waehrungen.EUR)
        );
    }

    protected override GirokontoDto DefaultMapToInput(Girokonto outputValue)
    {
        return new GirokontoDto(outputValue.Kontonummer.IBAN, outputValue.Kunde.Kundennummer.Nummer, outputValue.Kontostand.Wert);
    }
}

public class GirokontoDbToGirokontoDtoMapper : BaseMapper<GirokontoDbModel, GirokontoDto>
{
    protected override GirokontoDto DefaultMapToOutput(GirokontoDbModel inputValue)
    {
        return new GirokontoDto(inputValue.IBAN, inputValue.Kundennummer, inputValue.Kontostand);
    }

    protected override GirokontoDbModel DefaultMapToInput(GirokontoDto outputValue)
    {
        return new GirokontoDbModel(outputValue.IBAN, outputValue.Kundennummer, outputValue.Kontostand);
    }
}

public class KundeGirokontoToGirokontoMapper : DualInputToSingleOutputMapper<KundeDto, GirokontoDto, Girokonto>
{
    public KundeGirokontoToGirokontoMapper() : base((kundeDto, girokontoDto) =>
        new Girokonto(
            new Kunde(new Kundennummer(kundeDto.Kundennummer), kundeDto.Vorname, kundeDto.Nachname),
            new Kontonummer(girokontoDto.IBAN),
            new Betrag(girokontoDto.Kontostand, Waehrungen.EUR)
        ))
    {
    }
}

public class GirokontoToKundeGirokontoMapper : DualInputToSingleOutputMapper<Girokonto, Kunde, (KundeDto, GirokontoDto)>
{
    public GirokontoToKundeGirokontoMapper() : base((girokonto, kunde) =>
        (
            new KundeDto(girokonto.Kunde.Kundennummer.Nummer, girokonto.Kunde.Vorname, girokonto.Kunde.Nachname),
            new GirokontoDto(girokonto.Kontonummer.IBAN, girokonto.Kunde.Kundennummer.Nummer, girokonto.Kontostand.Wert)
        ))
    {
    }
}
