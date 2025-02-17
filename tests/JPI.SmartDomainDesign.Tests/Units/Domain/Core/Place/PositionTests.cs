using JPI.SmartDomainDesign.Domain.Core.Place;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using Shouldly;
using Xunit;

namespace JPI.SmartDomainDesign.Tests.Units.Domain.Core.Place;

public class PositionTests
{
    [Theory]
    [InlineData(180, 90)]
    [InlineData(180, -90)]
    [InlineData(-180, 90)]
    [InlineData(-180, -90)]
    [InlineData(-4.678230, 28.8758132)]
    public void NewPosition_ShouldCreatePosition(decimal longitude, decimal latitude)
    {
        // Arrange & Act
        var position = Position.CreateInstance(longitude, latitude);

        // Assert
        position.ShouldNotBeNull();
        position.Longitude.ShouldBeEquivalentTo(new Longitude(longitude));
        position.Latitude.ShouldBeEquivalentTo(new Latitude(latitude));
    }

    [Theory]
    [InlineData(181, 90, 1)]
    [InlineData(180, -91, 1)]
    [InlineData(181, -91, 2)]
    public void NotCorrectPosition_ShouldThrowException(decimal longitude, decimal latitude, int errorCount)
    {
        // Arrang & Act
        var exception = Should.Throw<PositionException>(() => Position.CreateInstance(longitude, latitude));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PositionException>();
        exception.ErrorCount.ShouldBe(errorCount);
    }
}
