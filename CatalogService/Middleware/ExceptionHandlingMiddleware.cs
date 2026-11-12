using FluentValidation;
using MoviePlatform.Exceptions;

namespace MoviePlatform.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await WriteError(context, StatusCodes.Status400BadRequest, ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (BadRequestException ex)
        {
            logger.LogWarning(ex, ex.Message);
            await WriteError(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (ResourceIsAlreadyExistException ex)
        {
            logger.LogWarning(ex, ex.Message);
            await WriteError(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (ResourceNotFoundException ex)
        {
            logger.LogWarning(ex, ex.Message);
            await WriteError(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteError(context, StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    private static Task WriteError(HttpContext context, int statusCode, object message)
    {
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsJsonAsync(new { status = statusCode, message });
    }
}