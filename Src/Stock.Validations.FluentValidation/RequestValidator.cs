using FluentValidation;
using Framework.Application.Validation;

namespace Stock.Validations.FluentValidation;

public class RequestValidator<TRequest>
    : AbstractValidator<TRequest>, IRequestValidator<TRequest>
    where TRequest : class
{
    public async Task<ValidationResult> ValidateAsync(TRequest request)
    {
        var result = await base.ValidateAsync(request);

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