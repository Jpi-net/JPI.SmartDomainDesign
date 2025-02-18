using JPI.SmartDomainDesign.Domain.Core.Place;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using Shouldly;
using Xunit;

namespace JPI.SmartDomainDesign.Tests.Units.Domain.Core.Place;

public class PostcodeTests
{
    [Fact]
    public void CreateInstance_ShouldCreatePostcode_WhenValidCodeAndLocalitiesProvided()
    {
        // Arrange
        var localities = new List<Locality>
        {
            Locality.CreateInstance("Paris", 48.8566m, 2.3522m)
        };

        long validCode = 7500;

        // Act
        var postcode = Postcode.CreateInstance(validCode, localities);

        // Assert
        postcode.ShouldNotBeNull();
        postcode.Code.ShouldBe(validCode);
        postcode.Cities.ShouldBeEquivalentTo(localities);
    }

    [Theory]
    [InlineData(999)]
    [InlineData(10000)]
    public void CreateInstance_ShouldThrowException_WhenCodeIsOutOfRange(long invalidCode)
    {
        // Act & Assert
        Should.Throw<PostCodeException>(() => Postcode.CreateInstance(invalidCode))
              .Message.ShouldContain("PostCode code should be between");
    }

    [Theory]
    [InlineData("Paris", "paris")]
    [InlineData("Bruxelles", "BruxeLLes   ")]
    public void CreateInstance_ShouldThrowException_WhenDuplicateLocalityNamesExist(string locality1, string locality2)
    {
        // Arrange
        var localities = new List<Locality>
        {
            Locality.CreateInstance(locality1, 48.8566m, 2.3522m),
            Locality.CreateInstance(locality2, 58.8566m, 2.3522m),
        };

        long validCode = 7500;

        // Act
        var exception = Should.Throw<PostCodeException>(() => Postcode.CreateInstance(validCode, localities));

        //  Assert
        var validationException = exception.InnerExceptions
            .OfType<PostCodeLocalitiesValidationException>()
            .FirstOrDefault();

        validationException.ShouldNotBeNull();
        validationException.InnerExceptions.ShouldContain(ex => ex is DuplicatePostCodeLocalitiesException);
    }

    [Fact]
    public void CreateInstance_ShouldThrowException_WhenDuplicatePositionsExist()
    {
        // Arrange
        var localities = new List<Locality>
        {
            Locality.CreateInstance("Paris", 48.8566m, 2.3522m),
            Locality.CreateInstance("Brussels", 48.8566m, 2.3522m),
        };

        long validCode = 7500;

        // Act
        var exception = Should.Throw<PostCodeException>(() => Postcode.CreateInstance(validCode, localities));

        //  Assert
        var validationException = exception.InnerExceptions
            .OfType<PostCodeLocalitiesValidationException>()
            .FirstOrDefault();

        validationException.ShouldNotBeNull();
        validationException.InnerExceptions.ShouldContain(ex => ex is DuplicatePostCodePositionsException);
    }

    [Fact]
    public void CreateInstance_ShouldThrowException_WhenDuplicatePositionsExist_And_DuplicateLocalityNamesExist()
    {
        // Arrange
        var localities = new List<Locality>
        {
            Locality.CreateInstance("Paris", 48.8566m, 2.3522m),
            Locality.CreateInstance("Brussels", 48.8566m, 2.3522m),
            Locality.CreateInstance("BruSsels  ", 2.8566m, 27.3522m),
        };

        long validCode = 7500;

        // Act
        var exception = Should.Throw<PostCodeException>(() => Postcode.CreateInstance(validCode, localities));

        //  Assert
        var validationException = exception.InnerExceptions
            .OfType<PostCodeLocalitiesValidationException>()
            .FirstOrDefault();

        validationException.ShouldNotBeNull();
        validationException.InnerExceptions.ShouldContain(ex => ex is DuplicatePostCodePositionsException);
        validationException.InnerExceptions.ShouldContain(ex => ex is DuplicatePostCodeLocalitiesException);
    }
}