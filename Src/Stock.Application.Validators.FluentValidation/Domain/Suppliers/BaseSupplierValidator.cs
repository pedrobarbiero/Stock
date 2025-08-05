using FluentValidation;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Domain.Models.Suppliers;

namespace Stock.Application.Validators.FluentValidation.Domain.Suppliers;

public abstract class BaseSupplierValidator<T> : RequestValidator<T> where T : BaseSupplierRequest
{
    protected BaseSupplierValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(SupplierConstants.MinNameLength, SupplierConstants.MaxNameLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(SupplierConstants.MaxEmailLength);
    }
}