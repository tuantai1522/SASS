using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SASS.Chassis.Exceptions;


public sealed class NotFoundException(string message) : Exception(message);

/// <summary>
/// Todo: To check PerRequestLogBuffer
/// This exception will catch not found exception
/// </summary>
public sealed class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        logger.LogWarning(
            exception,
            "[{Handler}] Not found exception occurred: {Message}",
            nameof(NotFoundExceptionHandler),
            notFoundException.Message
        );

        await Results.Problem(
            detail: exception.Message,
            statusCode: StatusCodes.Status404NotFound
        ).ExecuteAsync(httpContext);

        return true;
    }
}
