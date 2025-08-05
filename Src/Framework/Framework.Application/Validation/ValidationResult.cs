namespace Framework.Application.Validation;

public class ValidationResult
{
    protected ValidationResult(bool isValid, IDictionary<string, string[]>? errors = null)
    {
        IsValid = isValid;
        Errors = errors ?? new Dictionary<string, string[]>();
    }


    public bool IsValid { get; protected init; }
    public IDictionary<string, string[]> Errors { get; protected init; }

    public static ValidationResult Success() => new(true);

    public static ValidationResult Failure(IDictionary<string, string[]> errors) => new(false, errors);
}