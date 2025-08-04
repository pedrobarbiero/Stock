namespace Framework.Application.Validation;

public interface IRequestValidator<in TUseCase>
{
    Task<ValidationResult> ValidateAsync(TUseCase useCase);
}