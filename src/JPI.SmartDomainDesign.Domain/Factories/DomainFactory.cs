using JPI.SmartDomainDesign.Domain.Exceptions;

namespace JPI.SmartDomainDesign.Domain.Factories;

internal class DomainFactory
{
    /// <summary>
    /// Generic method to create a domain object while capturing and handling exceptions.
    /// </summary>
    /// <typeparam name="TResult">The type of the domain object to create.</typeparam>
    /// <typeparam name="TException">The type of exception to throw if errors are captured.</typeparam>
    /// <param name="factory">The function that creates the domain object.</param>
    /// <param name="errorMessage">The error message to use when throwing the exception.</param>
    /// <returns>The created domain object.</returns>
    /// <exception cref="TException">Thrown when one or more business exceptions are captured during object creation.</exception>
    public static TResult Create<TResult, TException>(
        Func<AggregateExceptionHandler, TResult> factory,
        string? errorMessage
    ) where TException : BusinessException
    {
        var handler = new AggregateExceptionHandler();
        var result = factory(handler);

        if (handler.BusinessExceptions.Count > 0)
        {
            var exceptionArgs = errorMessage is not null
                ? [errorMessage, handler.BusinessExceptions]
                : new object[] { handler.BusinessExceptions };

            throw (TException)Activator.CreateInstance(typeof(TException), exceptionArgs)!;
        }

        return result;
    }

    /// <summary>
    /// Generic method to perform validations while capturing and handling exceptions.
    /// </summary>
    /// <typeparam name="TException">The type of exception to throw if errors are captured.</typeparam>
    /// <param name="validateActions">The actions to perform for validation.</param>
    /// <param name="errorMessage">The error message to use when throwing the exception.</param>
    /// <exception cref="TException">Thrown when one or more business exceptions are captured during validation.</exception>
    public static void Validate<TException>(
        Action<AggregateExceptionHandler> validateActions,
        string? errorMessage
    ) where TException : BusinessException
    {
        var handler = new AggregateExceptionHandler();

        // Exécuter les validations
        validateActions(handler);

        // Si des exceptions sont présentes, les lever
        if (handler.BusinessExceptions.Count > 0)
        {
            var exceptionArgs = errorMessage is not null
                ? [errorMessage, handler.BusinessExceptions]
                : new object[] { handler.BusinessExceptions };

            throw (TException)Activator.CreateInstance(typeof(TException), exceptionArgs)!;
        }
    }
}
