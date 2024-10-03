using DomainDrivenDesign.Application.Kontoeroeffnung;
using DomainDrivenDesign.Domain.Kontoeroeffnung;
using DomainDrivenDesign.Helpers.Mappers;
using DomainDrivenDesign.Persistence.Kontoeroeffnung;

public class KundeToKundeDtoMapper : BaseMapper<Kunde, KundeDto>
{
    protected override KundeDto DefaultMapToOutput(Kunde kunde)
    {
        return new KundeDto(kunde.Kundennummer.Nummer, kunde.Vorname, kunde.Nachname);
    }

    protected override Kunde DefaultMapToInput(KundeDto kundeDto)
    {
        return new Kunde(new Kundennummer(kundeDto.Kundennummer), kundeDto.Vorname, kundeDto.Nachname);
    }
}

public class KundeDbToKundeDtoMapper : BaseMapper<KundeDbModel, KundeDto>
{
    protected override KundeDto DefaultMapToOutput(KundeDbModel kunde)
    {
        return new KundeDto(kunde.Kundennummer, kunde.Vorname, kunde.Nachname);
    }

    protected override KundeDbModel DefaultMapToInput(KundeDto kundeDto)
    {
        return new KundeDbModel(kundeDto.Kundennummer, kundeDto.Vorname, kundeDto.Nachname);
    }
}
