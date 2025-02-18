using JPI.SmartDomainDesign.Domain.Exceptions.Place;
using JPI.SmartDomainDesign.Domain.Factories;

namespace JPI.SmartDomainDesign.Domain.Core.Place;

public class Postcode
{
    // Business rules
    private static bool AllowsNullLocalities = true;
    private static int MinCode = 1000;
    private static int MaxCode = 9999;

    private IList<Locality> _localities;

    private Postcode(long code, ICollection<Locality>? localities)
    {
        Code = code;
        _localities = localities?.ToList() ?? [];
    }
    public long Code { get; }

    public IReadOnlyCollection<Locality>? Cities
        => _localities.ToList();

    private static void ValidateLocalities(ICollection<Locality>? localities = default)
    {
        if (!AllowsNullLocalities && localities is null)
        {
            throw new PostCodeException($"PostCode must have localities.");
        }

        if (localities is not null)
        {
            DomainFactory.Validate<PostCodeLocalitiesValidationException>(
            handler =>
            {
                var duplicateLocalities = localities
                  .GroupBy(l => l.Name.Trim().ToLower(System.Globalization.CultureInfo.CurrentCulture))
                  .Where(g => g.Count() > 1)
                  .Select(g => g.Key)
                  .ToList();

                handler.TryExecute(() =>
                {
                    if (duplicateLocalities.Count > 0)
                    {
                        throw new DuplicatePostCodeLocalitiesException(duplicateLocalities);
                    }
                });

                var duplicatePositions = localities
                    .GroupBy(l => new { l.Position.Latitude, l.Position.Longitude })
                    .Where(g => g.Count() > 1)
                    .Select(g => $"({g.Key.Latitude}, {g.Key.Longitude})")
                    .ToList();

                handler.TryExecute(() =>
                {
                    if (duplicatePositions.Count > 0)
                    {
                        throw new DuplicatePostCodePositionsException(duplicatePositions);
                    }
                });
            },
            "PostCode localities validation errors.");
        }
    }

    private static void ValidateCode(long code)
    {
        if (code < MinCode || code > MaxCode)
        {
            throw new PostCodeException($"PostCode code should be between {MinCode} & {MaxCode}. Code provided : {code}.");
        }
    }

    public static Postcode CreateInstance(long code, ICollection<Locality>? localities = default)
        => DomainFactory.Create<Postcode, PostCodeException>(
            handler =>
            {
                handler.TryExecute(() => ValidateLocalities(localities));
                handler.TryExecute(() => ValidateCode(code));

                return new Postcode(code, localities);
            },
            "Failed to create postcode due to invalid values");
}
