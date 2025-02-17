namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class LatitudeException
    : BusinessException
{
    public LatitudeException(decimal latitude)
        : base($"Wrong latitude value provided : {latitude}")
    {
    }

    public LatitudeException(string message)
        : base(message)
    {
    }

    public LatitudeException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public LatitudeException()
    {
    }
}
