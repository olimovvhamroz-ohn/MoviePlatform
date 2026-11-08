using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoviePlatform.Data;
using MoviePlatform.Entities;
using MoviePlatform.Exceptions;

namespace MoviePlatform.CQRS.Common;

public record UpdateEntityCommand<TEntity, TRequest, TResponse>(
    long Id,
    TRequest Data,
    Expression<Func<TEntity, bool>>? DuplicateFilter = null) : IRequest<TResponse>
    where TEntity : BaseEntity;

public class UpdateEntityCommandHandler<TEntity, TRequest, TResponse>(
    ApplicationContext context,
    IMapper mapper,
    IValidator<TRequest> validator,
    ILogger<UpdateEntityCommandHandler<TEntity, TRequest, TResponse>> logger)
    : IRequestHandler<UpdateEntityCommand<TEntity, TRequest, TResponse>, TResponse>
    where TEntity : BaseEntity
{
    public async Task<TResponse> Handle(
        UpdateEntityCommand<TEntity, TRequest, TResponse> request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request.Data, cancellationToken);
        if (!validation.IsValid)
            throw new BadRequestException(
                string.Join("; ", validation.Errors.Select(e => e.ErrorMessage)));

        var set = context.Set<TEntity>();

        var entity = await set.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException(
                $"{typeof(TEntity).Name} with id {request.Id} not found");

        if (request.DuplicateFilter != null &&
            await set.Where(x => x.Id != request.Id).AnyAsync(request.DuplicateFilter, cancellationToken))
            throw new ResourceIsAlreadyExistException(
                $"{typeof(TEntity).Name} with this value already exists");

        mapper.Map(request.Data, entity);
        await context.SaveChangesAsync(cancellationToken);

        logger.LogInformation("{Entity} {Id} updated", typeof(TEntity).Name, request.Id);
        return mapper.Map<TResponse>(entity);
    }
}