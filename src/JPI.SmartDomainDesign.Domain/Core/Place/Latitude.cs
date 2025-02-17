using JPI.SmartDomainDesign.Domain.Core.Abstractions;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;

namespace JPI.SmartDomainDesign.Domain.Core.Place;

public record Latitude
    : SmartValueBase<decimal>
{
    //Business Rules
    private const int Min = -90;
    private const int Max = 90;

    public Latitude(decimal value)
        : base(value)
    {
    }

    protected override bool OnValidate(decimal value)
        => value >= Min && value <= Max;

    protected override void OnValidationError(decimal value)
        => throw new LatitudeException(value);
}
