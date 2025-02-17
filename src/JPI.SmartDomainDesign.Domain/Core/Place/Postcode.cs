namespace JPI.SmartDomainDesign.Domain.Core.Place;

public class Postcode
{
    public long Code { get; }

    public ICollection<Locality>? Cities { get; }
}
