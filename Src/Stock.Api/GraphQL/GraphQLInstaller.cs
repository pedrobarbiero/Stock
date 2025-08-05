using Stock.Api.GraphQL.Suppliers;
using Stock.Api.GraphQL.Customers;

namespace Stock.Api.GraphQL;

public static class GraphQLInstaller
{
    public static IServiceCollection InstallGraphQL(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<SupplierQueries>()
            .AddTypeExtension<CustomerQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<SupplierMutations>()
            .AddTypeExtension<CustomerMutations>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}