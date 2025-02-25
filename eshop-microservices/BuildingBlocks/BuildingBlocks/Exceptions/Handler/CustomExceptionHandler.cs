using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError("ErrorMessage: {ExceptionMessage} || Time of occurrence: {TimeOfOccurrence}",
            exception.Message, DateTime.Now);

        var (detail, title, statusCode) = exception switch
        {
            BadRequestException badRequestException =>
            (
                badRequestException.Message,
                badRequestException.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            NotFoundException notFoundException =>
            (
                notFoundException.Message,
                notFoundException.GetType().Name,
                StatusCodes.Status404NotFound
            ),
            ValidationException validationException => (
                validationException.Message,
                validationException.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            _ => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            )
        };
        
        var problemDetails = new ProblemDetails
        {
            Detail = detail,
            Title = title,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };
        
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException ve)
        {
            problemDetails.Extensions.Add("validationErrors", ve.Errors);
        }
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}