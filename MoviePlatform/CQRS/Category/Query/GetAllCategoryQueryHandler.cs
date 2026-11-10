using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Category.Query;

public record GetAllCategoriesQuery(int Page, int PageSize) : IRequest<List<CategoryResponse>>;

public class GetAllCategoriesQueryHandler(
    ApplicationContext context) : IRequestHandler<GetAllCategoriesQuery, List<CategoryResponse>>
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

        return categories
            .Select(CategoryMapping.ToResponse)
            .ToList();
    }
}