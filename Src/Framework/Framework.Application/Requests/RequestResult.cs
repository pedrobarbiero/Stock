using Framework.Application.Validation;

namespace Framework.Application.Requests;

public class RequestResult<T>
{
    private RequestResult(T? data, IDictionary<string, string[]> errors)
    {
        Data = data;
        Errors = errors;
    }

    public T? Data { get; }
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => !IsSuccess;
    public IDictionary<string, string[]> Errors { get; }
    public static RequestResult<T> Success(T data) => new(data, new Dictionary<string, string[]>());
    public static RequestResult<T> Success() => new(default, new Dictionary<string, string[]>());

    public static RequestResult<T> Failure(ValidationResult validationResult) =>
        new(default, validationResult.Errors);

    public static RequestResult<T> NotFound(string className, Guid id) =>
        new(default, new Dictionary<string, string[]> { { "NotFound", [$"{className} not found.", id.ToString()] } });
}