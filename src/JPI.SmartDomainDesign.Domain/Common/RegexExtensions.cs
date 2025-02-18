using System.Text.RegularExpressions;

namespace JPI.SmartDomainDesign.Domain.Common;

internal static class RegexExtensions
{
    public static bool IsMatchNotNull(this Regex regex, string? input)
    {
        ArgumentNullException.ThrowIfNull(regex);

        return input is not null && regex.IsMatch(input);
    }
}
