namespace Framework.Application.Validation;

public record ValidationMessage
{
    public required string Field { get; init; }
    public required IEnumerable<string> Message { get; init; }
}