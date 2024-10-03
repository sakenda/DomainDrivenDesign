
using DomainDrivenDesign.Helpers.Exceptions;

namespace DomainDrivenDesign.Domain.Shared;

public class KundeNichtGefundenException : DomainException
{
    public KundeNichtGefundenException(string message) : base(message) { }
}