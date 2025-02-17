namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class LongitudeException
    : BusinessException
{
    public LongitudeException(decimal longitude)
        : base($"Wrong longitude value provided : {longitude}")
    {
    }

    public LongitudeException(string message)
        : base(message)
    {
    }

    public LongitudeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public LongitudeException()
    {
    }
}
