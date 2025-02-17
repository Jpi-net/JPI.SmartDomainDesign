using System.Globalization;
using System.Text;

namespace JPI.SmartDomainDesign.Domain.Exceptions;

public abstract class BusinessException
    : Exception
{
    public IReadOnlyCollection<BusinessException> InnerExceptions { get; private set; }
        = [];

    protected BusinessException() { }

    protected BusinessException(string message)
        : base(message) { }

    protected BusinessException(string message, Exception innerException)
        : base(message, innerException) { }

    protected BusinessException(IReadOnlyCollection<BusinessException> innerExceptions)
        => SetInnerExceptions(innerExceptions);

    protected BusinessException(string message, IReadOnlyCollection<BusinessException> innerExceptions)
        : base(message)
        => SetInnerExceptions(innerExceptions);

    public override string Message
        => InnerExceptions.Count > 0 ? FormatExceptionMessages() : base.Message;

    public int ErrorCount
        => InnerExceptions.Count > 0 ? InnerExceptions.Sum(inner => inner.ErrorCount) : 1;

    private void SetInnerExceptions(IReadOnlyCollection<BusinessException> innerExceptions)
    {
        if (innerExceptions is not null)
        {
            InnerExceptions = [.. innerExceptions];
        }
    }

    private string FormatExceptionMessages()
        => new StringBuilder()
        .AppendFormat(CultureInfo.InvariantCulture, "{0}: ", base.Message)
        .Append('{')
        .Append(string.Join(", ", InnerExceptions.Select(e => e.Message)))
        .Append('}')
        .ToString();
}