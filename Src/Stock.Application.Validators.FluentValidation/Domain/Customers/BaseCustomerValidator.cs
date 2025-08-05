using FluentValidation;
using Stock.Application.Features.Customers.Requests;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Validators.FluentValidation.Domain.Customers;

public abstract class BaseCustomerValidator<T> : RequestValidator<T> where T : BaseCustomerRequest
{
    protected BaseCustomerValidator()
    {
        RuleFor(x => x.Name)
            .Length(CustomerConstants.MinNameLength, CustomerConstants.MaxNameLength);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(CustomerConstants.MaxEmailLength);

        RuleFor(x => x.Addresses)
            .NotEmpty();
    }
}