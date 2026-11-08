using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Category.Command;

public record DeleteCategoryCommand(long Id) : IRequest<bool>;

public class DeleteCategoryCommandHandler(
    ApplicationContext context,
    ILogger<DeleteCategoryCommandHandler> logger) : IRequestHandler<DeleteCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
                           .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                       ?? throw new ResourceNotFoundException($"Category with id {request.Id} not found");

        var hasMovies = await context.Movies
            .AnyAsync(x => x.CategoryId == request.Id, cancellationToken);
        if (hasMovies)
            throw new BadRequestException("Category has movies and cannot be deleted");
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation($"Successful  Delete {request.Id} ");

        return true;
    }
}