# GraphQL Structure

This directory contains the GraphQL implementation for the Stock API, organized by AggregateRoot.

## Structure

```
GraphQL/
├── GraphQLInstaller.cs          # Extension method to configure GraphQL
├── Suppliers/                   # Supplier-specific GraphQL operations
│   ├── SupplierQueries.cs      # Supplier query operations
│   └── SupplierMutations.cs    # Supplier mutation operations
├── Customers/                   # Customer-specific GraphQL operations
│   ├── CustomerQueries.cs      # Customer query operations
│   └── CustomerMutations.cs    # Customer mutation operations
└── README.md                   # This file
```

## Adding New AggregateRoot GraphQL Operations

To add GraphQL support for a new AggregateRoot (e.g., Products):

### 1. Create Directory Structure
```bash
mkdir GraphQL/Products
```

### 2. Create Query Class
```csharp
// GraphQL/Products/ProductQueries.cs
[ExtendObjectType("Query")]
public class ProductQueries
{
    [UseProjection]
    [UseFiltering]  
    [UseSorting]
    public IQueryable<Product> GetProducts([Service] StockDbContext context) =>
        context.Products;

    public async Task<ProductResponse?> GetProductById(
        Guid id,
        [Service] IProductRepository productRepository,
        [Service] CreateProductMapper mapper)
    {
        var product = await productRepository.GetByIdAsync(id, CancellationToken.None);
        return product != null ? mapper.ToResponse(product) : null;
    }
}
```

### 3. Create Mutation Class
```csharp
// GraphQL/Products/ProductMutations.cs
[ExtendObjectType("Mutation")]
public class ProductMutations
{
    public async Task<ProductResponse?> CreateProduct(
        CreateProductRequest input,
        [Service] IProductService productService)
    {
        var result = await productService.CreateAsync(input, CancellationToken.None);
        return result.IsValid ? result.Data : null;
    }

    public async Task<ProductResponse?> UpdateProduct(
        Guid id,
        string name,
        decimal price,
        [Service] IProductService productService)
    {
        var updateRequest = new UpdateProductRequest { Id = id, Name = name, Price = price };
        var result = await productService.UpdateAsync(updateRequest, CancellationToken.None);
        return result.IsValid ? result.Data : null;
    }

    public async Task<bool> DeleteProduct(
        Guid id,
        [Service] IProductService productService)
    {
        var result = await productService.DeleteAsync(id, CancellationToken.None);
        return result.IsValid;
    }
}
```

### 4. Register in GraphQLInstaller
```csharp
// GraphQLInstaller.cs
using Stock.Api.GraphQL.Products;

public static IServiceCollection InstallGraphQL(this IServiceCollection services)
{
    services
        .AddGraphQLServer()
        .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<SupplierQueries>()
        .AddTypeExtension<CustomerQueries>()
        .AddTypeExtension<ProductQueries>()        // Add this
        .AddMutationType(d => d.Name("Mutation"))
        .AddTypeExtension<SupplierMutations>()
        .AddTypeExtension<CustomerMutations>()
        .AddTypeExtension<ProductMutations>()      // Add this
        .AddProjections()
        .AddFiltering()
        .AddSorting();

    return services;
}
```

## Available Operations

### Suppliers
- **Queries:**
  - `suppliers` - Get all suppliers with filtering, sorting, and projection
  - `supplierById(id: Guid)` - Get supplier by ID

- **Mutations:**
  - `createSupplier(input: CreateSupplierRequest)` - Create new supplier
  - `updateSupplier(id: Guid, name: String, email: String)` - Update supplier
  - `deleteSupplier(id: Guid)` - Delete supplier

### Customers
- **Queries:**
  - `customers` - Get all customers with filtering, sorting, and projection
  - `customerById(id: Guid)` - Get customer by ID

- **Mutations:**
  - `createCustomer(input: CreateCustomerRequest)` - Create new customer
  - `updateCustomer(id: Guid, name: String, email: String)` - Update customer
  - `deleteCustomer(id: Guid)` - Delete customer

## Example Queries

### Get All Suppliers
```graphql
query {
  suppliers {
    id
    name
    email
  }
}
```

### Get Suppliers with Filtering
```graphql
query {
  suppliers(where: { name: { contains: "Acme" } }) {
    id
    name
    email
  }
}
```

### Create Supplier
```graphql
mutation {
  createSupplier(input: { name: "New Supplier", email: "contact@supplier.com" }) {
    id
    name
    email
  }
}
```

### Update Supplier
```graphql
mutation {
  updateSupplier(id: "guid-here", name: "Updated Name", email: "new@email.com") {
    id
    name
    email
  }
}
```

## GraphQL Endpoint

- **URL:** `http://localhost:5003/graphql`
- **GraphQL Playground:** Available in development mode