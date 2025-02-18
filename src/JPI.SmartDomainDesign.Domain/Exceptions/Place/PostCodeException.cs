namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class PostCodeException
    : BusinessException
{
    public PostCodeException(string message, IReadOnlyCollection<BusinessException> innerExceptions)
        : base(message, innerExceptions)
    {
    }

    public PostCodeException(string message) : base(message)
    {
    }

    public PostCodeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public PostCodeException()
    {
    }
}
