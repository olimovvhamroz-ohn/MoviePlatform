using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.DTO.Request;
using MoviePlatform.DTO.Response;
using MoviePlatform.Entities;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Studio.Command;

public record CreateStudioCommand(StudioRequest Studio) : IRequest<StudioResponse>;

public class CreateStudioCommandHandler(
    ApplicationContext context,
    IMapper mapper,
    ILogger<CreateStudioCommandHandler> logger,
    IValidator<StudioRequest> validator) : IRequestHandler<CreateStudioCommand, StudioResponse>
{
    public async Task<StudioResponse> Handle(CreateStudioCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.Studio, cancellationToken);
        if (!validation.IsValid)
            throw new BadRequestException(
                string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

        var isDuplicate = await context.Studios.AnyAsync(
            x => x.Name.ToLower() == request.Studio.Name.ToLower(), cancellationToken);
        if (isDuplicate)
            throw new ResourceIsAlreadyExistException("Studio with this name already exists");

        var studio = mapper.Map<StudioEntity>(request.Studio);

        context.Studios.Add(studio);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Studio {Id} created", studio.Id);
        return mapper.Map<StudioResponse>(studio);
    }
}