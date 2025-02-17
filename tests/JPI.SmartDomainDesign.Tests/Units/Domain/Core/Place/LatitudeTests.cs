using JPI.SmartDomainDesign.Domain.Core.Place;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using Shouldly;
using Xunit;

namespace JPI.SmartDomainDesign.Tests.Units.Domain.Core.Place;

public class LatitudeTests
{
    [Theory]
    [InlineData(-90)]
    [InlineData(90)]
    public void NewLatitude_ShouldCreateLatitude(decimal latitude)
    {
        // Arrange & Act
        var result = new Latitude(latitude);

        // Assert
        result.ShouldNotBeNull();
        result.Value.ShouldBe(latitude);
    }

    [Fact]
    public void SameLatitudes_ShouldBeEquals()
    {
        // Arrange
        var latitude = new Latitude(10);
        var latitudeEquals = new Latitude(10);

        // Act &Assert
        latitude.ShouldBeEquivalentTo(latitudeEquals);
    }

    [Theory]
    [InlineData(-91)]
    [InlineData(91)]
    public void NotCorrectLatitude_ShouldThrowException(decimal latitude)
    {
        // Arrange, Act & Assert
        Should.Throw<LatitudeException>(() => new Latitude(latitude));
    }
}
