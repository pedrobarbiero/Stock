using Framework.Application.Services;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Features.Customers.Services;

public interface ICustomerService :
    ICreateService<Customer, CreateCustomerRequest, CustomerResponse>,
    IUpdateService<Customer, UpdateCustomerRequest, CustomerResponse>,
    IDeleteService<Customer>;