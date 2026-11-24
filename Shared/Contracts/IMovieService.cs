using MagicOnion;
using Shared.DTOs;

namespace Shared.Contracts;

public interface IMovieService : IService<IMovieService>
{
    UnaryResult<MovieDto?> GetMovieById(long id);

    UnaryResult<List<MovieDto>> GetMoviesByIds(HashSet<long> ids);
}