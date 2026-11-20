using Grpc.Net.Client;
using MagicOnion.Client;
using Shared.Contracts;
using Shared.DTOs;

namespace MoviePlatform.Grpc;

public class UserGrpcClient
{
    private readonly IUserService _client;

    public UserGrpcClient(IConfiguration  configuration)
    {
        var address = configuration["GrpcSettings:UserServiceUrl"];
        var channel = GrpcChannel.ForAddress(address!);
        _client = MagicOnionClient.Create<IUserService>(channel);
    }

    public async Task<UserDto?> GetUserById(long id)
    {
        return await _client.GetUserById(id);
    }
}