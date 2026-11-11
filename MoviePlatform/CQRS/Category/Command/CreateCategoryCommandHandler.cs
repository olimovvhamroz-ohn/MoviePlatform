using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Category.Command;

public record CreateCategoryCommand(CategoryRequest Request) : IRequest<CategoryResponse>;

public class CreateCategoryCommandHandler(
    ApplicationContext context,

    ILogger<CreateCategoryCommandHandler> loger) : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
       
        var isDublicate = await context.Categories.AnyAsync(x => x.Title.ToLower() == request.Request.Title.ToLower(),
            cancellationToken);

        if (isDublicate)
        {
            throw new ResourceIsAlreadyExistException("The record already exists");
        }

        var category = CategoryMapping.ToEntity(request.Request);
        context.Add(category);
        await context.SaveChangesAsync(cancellationToken);
        loger.LogInformation("Category created: {Id}", category.Id);
        return CategoryMapping.ToResponse(category);
    }
}