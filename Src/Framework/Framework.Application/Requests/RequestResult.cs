using Framework.Application.Validation;

namespace Framework.Application.Requests;

public class RequestResult<T>
{
    private RequestResult(T? data, IDictionary<string, string[]> errors, ushort statusCode)
    {
        Data = data;
        Errors = errors;
        StatusCode = statusCode;
    }

    public T? Data { get; }
    public bool IsValid => Errors.Count == 0;
    public ushort StatusCode { get; }
    public IDictionary<string, string[]> Errors { get; }
    public static RequestResult<T> Ok(T data) => new(data, new Dictionary<string, string[]>(), 200);
    public static RequestResult<T> Created(T data) => new(data, new Dictionary<string, string[]>(), 201);
    public static RequestResult<T> NoContent() => new(default, new Dictionary<string, string[]>(), 204);

    public static RequestResult<T> BadRequest(ValidationResult validationResult) =>
        new(default, validationResult.Errors, 400);

    public static RequestResult<T> NotFound(string className, Guid id) =>
        new(default, new Dictionary<string, string[]> { { "NotFound", [$"{className} not found.", id.ToString()] } },
            404);
}