using FluentValidation;
using UserService.Exceptions;

namespace UserService.Middleware;

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
        catch (ResourceNotFoundException ex)
        {
            await WriteError(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (ResourceIsAlreadyExistException ex)
        {
            await WriteError(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            await WriteError(context, StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    private static Task WriteError(HttpContext context, int statusCode, object errors)
    {
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsJsonAsync(new { status = statusCode, errors });
    }
}