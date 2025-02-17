using JPI.SmartDomainDesign.Domain.Exceptions;

/// <summary>
/// Handles the aggregation of business exceptions during domain object creation or operations.
/// </summary>
internal class AggregateExceptionHandler
{
    /// <summary>
    /// Holds the list of captured business exceptions.
    /// </summary>
    private readonly List<BusinessException> _exceptions = [];

    /// <summary>
    /// Attempts to execute the specified function and captures any thrown BusinessException.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="action">The function to execute.</param>
    /// <returns>The result of the function if successful; otherwise, the default value of type T.</returns>
    public T? TryExecute<T>(Func<T> action)
    {
        if (action is null)
        {
            return default;
        }

        try
        {
            return action();
        }
        catch (BusinessException ex)
        {
            _exceptions.Add(ex);
            return default;
        }
    }

    /// <summary>
    /// Attempts to execute the specified action and captures any thrown BusinessException.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public void TryExecute(Action action)
    {
        try
        {
            action();
        }
        catch (BusinessException ex)
        {
            _exceptions.Add(ex);
        }
    }

    /// <summary>
    /// Gets the collection of captured business exceptions.
    /// </summary>
    public IReadOnlyCollection<BusinessException> BusinessExceptions => _exceptions.AsReadOnly();
}