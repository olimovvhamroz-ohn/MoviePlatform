using Grpc.Net.Client;
using MagicOnion.Client;
using Shared.Contracts;
using Shared.DTOs;

namespace EngagementService.Grpc;

public class MovieGrpcClient
{
    private readonly IMovieService _client;

    public MovieGrpcClient(IConfiguration configuration)
    {
        var address = configuration["GrpcSettings:CatalogServiceUrl"];

        var channel = GrpcChannel.ForAddress(address!);

        _client = MagicOnionClient.Create<IMovieService>(channel);
    }

    public async Task<MovieDto?> GetMovieById(long id)
    {
        return await _client.GetMovieById(id);
    }

    public async Task<List<MovieDto>> GetMoviesByIds(HashSet<long> ids)
    {
        return await _client.GetMoviesByIds(ids);
    }
}