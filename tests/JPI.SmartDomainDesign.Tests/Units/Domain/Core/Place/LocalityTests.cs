using JPI.SmartDomainDesign.Domain.Core.Place;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using Shouldly;
using Xunit;

namespace JPI.SmartDomainDesign.Tests.Units.Domain.Core.Place;

public class LocalityTests
{
    [Theory]
    [InlineData("Kkjdq dbjq - dqug", 180, 90)]
    [InlineData("rsa", 180, -90)]
    [InlineData("Choco . Ane", -180, 90)]
    [InlineData("L'agrange", -180, -90)]
    public void NewLocality_ShouldCreateLocality(string name, decimal longitude, decimal latitude)
    {
        // Arrange & Act
        var locality = Locality.CreateInstance(name, longitude, latitude);

        // Assert
        locality.ShouldNotBeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("d")]
    [InlineData("ml")]
    [InlineData("x ")]
    [InlineData("     ")]
    [InlineData("lsq@SQ")]
    [InlineData("!")]
    [InlineData("Hell!")]
    [InlineData("City09")]
    [InlineData("Epr&ace")]
    [InlineData(" te  ")]
    public void InvalideName_ShouldThrowException(string? name)
    {
        // Arrange
        var longitude = 0m;
        var latitude = 0m;

        // Act
        var exception = Should.Throw<LocalityException>(() => Locality.CreateInstance(name!, longitude, latitude));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<LocalityException>();
    }

    [Theory]
    [InlineData("", 180, 90, 3)]
    [InlineData(null, 181, 90, 3)]
    [InlineData("d", -200, 94, 3)]
    [InlineData("     ", 180, 90, 2)]
    [InlineData("H!", 181, -92, 4)]
    public void InvalideValues_ShouldThrowException(string? name, decimal longitude, decimal latitude, int errorCount)
    {
        // Arrange & Act
        var exception = Should.Throw<LocalityException>(() => Locality.CreateInstance(name!, longitude, latitude));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<LocalityException>();
        exception.ErrorCount.ShouldBe(errorCount);
    }
}
