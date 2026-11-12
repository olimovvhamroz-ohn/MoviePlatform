using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Exceptions;
using MoviePlatform.Mapping;

namespace MoviePlatform.CQRS.Studio.Command;

public record CreateStudioCommand(StudioRequest Studio) : IRequest<StudioResponse>;

public class CreateStudioCommandHandler(
    ApplicationContext context,
    IMapper mapper,
    ILogger<CreateStudioCommandHandler> logger) : IRequestHandler<CreateStudioCommand, StudioResponse>
{
    public async Task<StudioResponse> Handle(CreateStudioCommand request, CancellationToken cancellationToken)
    {
       
        var isDuplicate = await context.Studios.AnyAsync(
            x => x.Name.ToLower() == request.Studio.Name.ToLower(), cancellationToken);

        if (isDuplicate)
        {
            throw new ResourceIsAlreadyExistException("Studio with this name already exists");
        }

        var studio = StudioMapping.ToEntity(request.Studio);
        context.Studios.Add(studio);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Studio {Id} created", studio.Id);

        return StudioMapping.ToResponse(studio);    }
}