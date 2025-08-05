using Framework.Application.Validation;

namespace Framework.Application.Requests;

public class RequestResult<T>
{
    private RequestResult(T? data, IDictionary<string, string[]> errors, bool isValid, ushort statusCode)
    {
        Data = data;
        Errors = errors;
        IsValid = isValid;
        StatusCode = statusCode;
    }

    public T? Data { get; }
    public bool IsValid { get; }
    public ushort StatusCode { get; }
    public IDictionary<string, string[]> Errors { get; }
    public static RequestResult<T> Ok(T data) => new(data, new Dictionary<string, string[]>(), true, 200);
    public static RequestResult<T> Created(T data) => new(data, new Dictionary<string, string[]>(), true, 201);
    public static RequestResult<T> NoContent() => new(default(T), new Dictionary<string, string[]>(), true, 204);

    public static RequestResult<T> BadRequest(ValidationResult validationResult) =>
        new(default, validationResult.Errors, false, 400);

    public static RequestResult<T> NotFound(string className, Guid id) =>
        new(default, new Dictionary<string, string[]> { { "NotFound", [$"{className} not found.", id.ToString()] } },
            false, 404);
}