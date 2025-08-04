using Framework.Application.Validation;

namespace Framework.Application.Requests;

public class RequestResult<T>
{
    private RequestResult(IDictionary<string, string[]> errors)
    {
        IsValid = false;
        Errors = errors;
    }

    private RequestResult(T data)
    {
        Data = data;
        Errors = new Dictionary<string, string[]>();
        IsValid = true;
    }

    private RequestResult(ValidationResult validationResult, T? data)
    {
        IsValid = validationResult.IsValid;
        Errors = validationResult.Errors;
        Data = data;
    }

    public T? Data { get; }
    public bool IsValid { get; }
    public IDictionary<string, string[]> Errors { get; }
    public static RequestResult<T> Success(T data) => new(data);
    public static RequestResult<T> Failure(ValidationResult validationResult) => new(validationResult.Errors);

    public static RequestResult<T> NotFound(string className, Guid id) =>
        new(new NotFoundValidationResult(className, id), default);
}