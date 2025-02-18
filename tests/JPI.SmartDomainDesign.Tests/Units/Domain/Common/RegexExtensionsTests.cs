using System.Text.RegularExpressions;
using JPI.SmartDomainDesign.Domain.Common;
using Shouldly;
using Xunit;

namespace JPI.SmartDomainDesign.Tests.Units.Domain.Common;

public class RegexExtensionsTests
{
    [Fact]
    public void IsMatchNotNull_ShouldReturnTrue_WhenInputMatchesPattern()
    {
        // Arrange
        var regex = new Regex(@"^[a-zA-Z\s\-.'À-ÖØ-öø-ÿ]+$");
        string input = "Jean Dupont";

        // Act
        bool result = regex.IsMatchNotNull(input);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsMatchNotNull_ShouldReturnFalse_WhenInputDoesNotMatchPattern()
    {
        // Arrange
        var regex = new Regex(@"^[a-zA-Z\s\-.'À-ÖØ-öø-ÿ]+$");
        string input = "Jean123"; // Contains numbers, so it's invalid

        // Act
        bool result = regex.IsMatchNotNull(input);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsMatchNotNull_ShouldReturnFalse_WhenInputIsNull()
    {
        // Arrange
        var regex = new Regex(@"^[a-zA-Z\s\-.'À-ÖØ-öø-ÿ]+$");
        string? input = null;

        // Act
        bool result = regex.IsMatchNotNull(input);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsMatchNotNull_ShouldThrowArgumentNullException_WhenRegexIsNull()
    {
        // Arrange
        Regex? regex = null;
        string input = "Jean Dupont";

        // Act & Assert
        Should.Throw<ArgumentNullException>(() => regex!.IsMatchNotNull(input))
            .ParamName.ShouldBe("regex");
    }
}