namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class PositionException
    : BusinessException
{
    public PositionException(string message, IReadOnlyCollection<BusinessException> innerExceptions)
        : base(message, innerExceptions)
    {
    }

    public PositionException(string message)
        : base(message)
    {
    }

    public PositionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public PositionException()
    {
    }
}
