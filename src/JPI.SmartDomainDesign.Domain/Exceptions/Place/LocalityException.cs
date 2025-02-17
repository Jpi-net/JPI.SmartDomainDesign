namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class LocalityException
    : BusinessException
{
    public LocalityException(string message, IReadOnlyCollection<BusinessException> innerExceptions)
        : base(message, innerExceptions)
    {
    }

    public LocalityException(string message) : base(message)
    {
    }

    public LocalityException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public LocalityException()
    {
    }
}
