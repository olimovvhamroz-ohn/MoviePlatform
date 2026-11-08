using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Category.Command;

public record CreateCategoryCommand(CategoryRequest Request) : IRequest<CategoryResponse>;

public class CreateCategoryCommandHandler(
    ApplicationContext context,
    IMapper mapper,
    ILogger<CreateCategoryCommandHandler> loger,
    IValidator<CategoryRequest> validator) : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.Request, cancellationToken);
        if (!validation.IsValid)
            throw new BadRequestException("validation.Errors");
        var isDublicate = await context.Categories.AnyAsync(x => x.Title.ToLower() == request.Request.Title.ToLower(),
            cancellationToken);

        if (isDublicate)
            throw new ResourceIsAlreadyExistException("The record already exists");

        var category = mapper.Map<CategoryEntity>(request.Request);

        context.Add(category);
        await context.SaveChangesAsync(cancellationToken);
        loger.LogInformation("Category created: {Id}", category.Id);
        return mapper.Map<CategoryResponse>(category);
    }
}