using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;

namespace MoviePlatform.CQRS.Category.Query;

public record GetAllCategoriesQuery(int Page, int PageSize) : IRequest<List<CategoryResponse>>;

public class GetAllCategoriesQueryHandler(
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
{
    public async Task<List<CategoryResponse>> Handle(
        GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Title)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return mapper.Map<List<CategoryResponse>>(categories);
    }
}