using JPI.SmartDomainDesign.Domain.Core.Place;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using Shouldly;
using Xunit;

namespace JPI.SmartDomainDesign.Tests.Units.Domain.Core.Place;

public class LongitudeTests
{
    [Theory]
    [InlineData(-180)]
    [InlineData(180)]
    public void NewLongitude_ShouldCreateLongitude(decimal longitude)
    {
        // Arrange & Act
        var result = new Longitude(longitude);

        // Assert
        result.Value.ShouldBe(longitude);
    }

    [Fact]
    public void SameLongitudes_ShouldBeEquals()
    {
        // Arrange
        var longitude = new Longitude(10);
        var longitudeEquals = new Longitude(10);

        // Act &Assert
        longitude.ShouldBeEquivalentTo(longitudeEquals);
    }

    [Theory]
    [InlineData(-181)]
    [InlineData(181)]
    public void NotCorrectLongitude_ShouldThrowException(decimal longitude)
    {
        // Arrange, Act & Assert
        Should.Throw<LongitudeException>(() => new Longitude(longitude));
    }
}
