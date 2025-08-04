using FluentValidation;
using Framework.Application.Validation;

namespace Stock.Validations.FluentValidation;

public class RequestValidator<TUseCase>
    : AbstractValidator<TUseCase>, IRequestValidator<TUseCase>
    where TUseCase : class
{
    public async Task<ValidationResult> ValidateAsync(TUseCase useCase)
    {
        var result = await base.ValidateAsync(useCase);

        if (result.IsValid)
            return ValidationResult.Success();

        var errors = result.Errors
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .Select(failureGroup => new
            {
                Field = failureGroup.Key,
                Message = failureGroup.ToArray()
            })
            .ToDictionary(k => k.Field, v => v.Message);

        return ValidationResult.Failure(errors);
    }
}