using JPI.SmartDomainDesign.Domain.Common;
using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using JPI.SmartDomainDesign.Domain.Factories;
using System.Text.RegularExpressions;

namespace JPI.SmartDomainDesign.Domain.Core.Place;

public class Locality
{
    // Business rules
    private static Regex NameRegex = new Regex(@"^[a-zA-Z\s\-.'À-ÖØ-öø-ÿ]+$");
    private static int MinNameLenght = 3;

    private Locality (string name, Position position)
    {
        Name = NormalizeName(name);
        Position = position;
    }

    public string Name { get; }

    public Position Position { get; }

    private static void ValidateName(string name)
        => DomainFactory.Validate<LocalityException>(
            handler =>
            {
                handler.TryExecute(() =>
                {
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        throw new LocalityException("The locality name cannot be null, empty, or whitespace");
                    }
                });
                handler.TryExecute(() =>
                {
                    if (!NameRegex.IsMatchNotNull(name))
                    {
                        throw new LocalityException("The locality name contains invalid characters. Only letters, spaces, hyphens (-), apostrophes ('), and periods (.) are allowed.");
                    }
                });
                handler.TryExecute(() =>
                {
                    if (name?.Trim().Length < MinNameLenght)
                    {
                        throw new LocalityException("The locality name must be at least 3 characters long after trimming");
                    }
                });
            },
            "Invalid locality name value");

    private static string NormalizeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < MinNameLenght)
        {
            return name;
        }
        name = name.Trim();
        return $"{char.ToUpper(name[0], System.Globalization.CultureInfo.InvariantCulture)}{name[1..].ToLower(System.Globalization.CultureInfo.CurrentCulture)}";
    }

    public static Locality CreateInstance (string name, decimal longitude, decimal latitude)
        => DomainFactory.Create<Locality, LocalityException>(
            handler =>
            {
                var position = handler.TryExecute(() => Position.CreateInstance(longitude, latitude));
                handler.TryExecute(() => ValidateName(name));
                return position != null ?
                    new Locality(name, position) :
                    default!;
            },
            "Failed to create locality due to invalid values");
}
