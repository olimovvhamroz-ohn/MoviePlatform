using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.CQRS.Category.Command;
using MoviePlatform.CQRS.Category.Query;
using MoviePlatform.CQRS.Common;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;

namespace MoviePlatform.Controller;

[ApiController]
[Route("api/categories")]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CategoryResponse>> Create(CategoryRequest request)
    {
        var createCategory = await mediator.Send(new CreateCategoryCommand(request));
        return Ok(createCategory);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CategoryResponse>> GetById(long id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetCategoryByIdQuery(id), cancellationToken));

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponse>>> GetAll(
        int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetAllCategoriesQuery(page, pageSize), cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<ActionResult<CategoryResponse>> Update(
        long id, CategoryRequest request, CancellationToken ct)
        => Ok(await mediator.Send(
            new UpdateEntityCommand<CategoryEntity, CategoryRequest, CategoryResponse>(
                id, request,
                x => x.Title.ToLower() == request.Title.ToLower()), ct));
}