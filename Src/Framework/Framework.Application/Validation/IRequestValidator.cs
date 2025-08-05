namespace Framework.Application.Validation;

public interface IRequestValidator<in TRequest>
{
    Task<ValidationResult> ValidateAsync(TRequest request, CancellationToken cancellationToken);
}