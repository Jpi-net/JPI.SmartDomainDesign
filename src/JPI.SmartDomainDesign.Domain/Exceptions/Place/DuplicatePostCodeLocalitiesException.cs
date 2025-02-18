namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class DuplicatePostCodeLocalitiesException
    : BusinessException
{
    public DuplicatePostCodeLocalitiesException(ICollection<string> duplicateLocalities)
        : base($"Duplicated localities name not allowed in the same postcode : {string.Join(", ", duplicateLocalities)}")
    {
    }

    public DuplicatePostCodeLocalitiesException(string message)
        : base(message)
    {
    }

    public DuplicatePostCodeLocalitiesException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DuplicatePostCodeLocalitiesException()
    {
    }
}
