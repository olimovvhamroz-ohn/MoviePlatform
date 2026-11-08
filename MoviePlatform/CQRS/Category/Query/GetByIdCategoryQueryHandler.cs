using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Category.Query;

public record GetCategoryByIdQuery(long Id) : IRequest<CategoryResponse>;

public class GetCategoryByIdQueryHandler(
    ApplicationContext context,
    IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
                           .AsNoTracking()
                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                       ?? throw new ResourceNotFoundException($"Category with id {request.Id} not found");

        return mapper.Map<CategoryResponse>(category);
    }
}