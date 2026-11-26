using Grpc.Net.Client;
using MagicOnion.Client;
using Microsoft.Extensions.DependencyInjection;
using Shared.Contracts;

namespace ApiClient.CatalogService;

public static class CatalogServiceExtensions
{
    public static IServiceCollection AddCatalogServiceClient(
        this IServiceCollection services, string address)
    {
        services.AddSingleton<IMovieService>(x=>
            MagicOnionClient.Create<IMovieService>(GrpcChannel.ForAddress(address)));
        return services;
    }
}