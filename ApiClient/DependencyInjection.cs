using ApiClient.CatalogService;
using Microsoft.Extensions.DependencyInjection;

namespace ApiClient;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogService(
        this IServiceCollection services,string address)
    {
        services.AddCatalogServiceClient(address);
        return services;
    }
}

