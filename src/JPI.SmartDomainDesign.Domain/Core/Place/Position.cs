using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using JPI.SmartDomainDesign.Domain.Factories;

namespace JPI.SmartDomainDesign.Domain.Core.Place;

public record Position
{
    private Position(Longitude longitude, Latitude latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }

    public Longitude Longitude { get; }

    public Latitude Latitude { get; }

    public static Position CreateInstance (decimal longitude, decimal latitude)
        => DomainFactory.Create<Position, PositionException>(
            handler =>
            {
                var longitudeValue = handler.TryExecute(() => new Longitude(longitude));
                var latitudeValue = handler.TryExecute(() => new Latitude(latitude));
                return longitudeValue != null && latitudeValue != null ?
                    new Position(longitudeValue, latitudeValue) :
                    default!;
            },
            "Failed to create Position due to invalid values");
}
