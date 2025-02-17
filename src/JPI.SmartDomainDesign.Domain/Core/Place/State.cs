namespace JPI.SmartDomainDesign.Domain.Core.Place;

public class State
{
    public string? Name { get; }

    public string? Code { get; }

    public ICollection<Postcode>? Postcodes { get; }
}
