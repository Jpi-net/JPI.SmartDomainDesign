using JPI.SmartDomainDesign.Domain.Core.Abstractions;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;

namespace JPI.SmartDomainDesign.Domain.Core.Place;

public record Longitude
    : SmartValueBase<decimal>
{
    //Business Rules
    private const decimal Min = -180;
    private const decimal Max = 180;

    public Longitude(decimal value) : base(value)
    {
    }

    protected override bool OnValidate(decimal value)
        => value >= Min && value <= Max;

    protected override void OnValidationError(decimal value)
        => throw new LongitudeException(value);
}
