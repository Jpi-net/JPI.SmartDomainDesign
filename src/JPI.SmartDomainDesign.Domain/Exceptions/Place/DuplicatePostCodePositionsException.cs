using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPI.SmartDomainDesign.Domain.Exceptions.Place;

public class DuplicatePostCodePositionsException
    : BusinessException
{
    public DuplicatePostCodePositionsException(ICollection<string> duplicatePositions)
        : base($"Duplicate locality positions found: {string.Join(", ", duplicatePositions)}")
    {
    }

    public DuplicatePostCodePositionsException(string message)
        : base(message)
    {
    }

    public DuplicatePostCodePositionsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DuplicatePostCodePositionsException()
    {
    }
}