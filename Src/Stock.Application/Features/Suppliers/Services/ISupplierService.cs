using Framework.Application.Services;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;
using Stock.Domain.Models.Suppliers;

namespace Stock.Application.Features.Suppliers.Services;

public interface ISupplierService :
    ICreateService<Supplier, CreateSupplierRequest, SupplierResponse>,
    IUpdateService<Supplier, UpdateSupplierRequest, SupplierResponse>,
    IDeleteService<Supplier>;