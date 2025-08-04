namespace Framework.Domain.Exceptions;

public abstract class DomainException(string message) : Exception(message)
{
    public DomainException(string message, IDictionary<string, string[]> errors) : this(message) => Errors = errors;

    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
}