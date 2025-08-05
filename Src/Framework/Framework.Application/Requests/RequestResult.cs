using Framework.Application.Validation;

namespace Framework.Application.Requests;

public class RequestResult<T>
{
    private RequestResult(IDictionary<string, string[]> errors, ushort statusCode)
    {
        IsValid = false;
        Errors = errors;
        Data = default;
        StatusCode = statusCode;
    }

    private RequestResult(T data, ushort statusCode)
    {
        Data = data;
        Errors = new Dictionary<string, string[]>();
        IsValid = true;
        StatusCode = statusCode;
    }

    private RequestResult(ValidationResult validationResult, T? data, ushort statusCode)
    {
        IsValid = validationResult.IsValid;
        Errors = validationResult.Errors;
        Data = data;
        StatusCode = statusCode;
    }

    public T? Data { get; }
    public bool IsValid { get; }
    public ushort StatusCode { get; }
    public IDictionary<string, string[]> Errors { get; }
    public static RequestResult<T> Ok(T data) => new(data, 200);
    public static RequestResult<T> Created(T data) => new(data, 201);
    public static RequestResult<T> NoContent() => new(default(T)!, 204);
    public static RequestResult<T> BadRequest(ValidationResult validationResult) => new(validationResult.Errors, 400);

    public static RequestResult<T> NotFound(string className, Guid id) =>
        new(new NotFoundValidationResult(className, id), default, 404);
}