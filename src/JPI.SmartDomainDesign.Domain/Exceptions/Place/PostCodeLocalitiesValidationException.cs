namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class PostCodeLocalitiesValidationException
    : BusinessException
{
    public PostCodeLocalitiesValidationException(string message, IReadOnlyCollection<BusinessException> innerExceptions)
        : base(message, innerExceptions)
    {
    }

    public PostCodeLocalitiesValidationException(string message) : base(message)
    {
    }

    public PostCodeLocalitiesValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public PostCodeLocalitiesValidationException()
    {
    }
}
