using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SASS.Chassis.Exceptions;


public class ConflictException(string message) : Exception(message);

/// <summary>
/// Todo: To check PerRequestLogBuffer
/// This exception will catch conflict exception
/// </summary>
public sealed class ConflictExceptionHandler(ILogger<ConflictExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ConflictException conflictException)
        {
            return false;
        }

        logger.LogWarning(
            exception,
            "[{Handler}] Conflict exception occurred: {Message}",
            nameof(ConflictExceptionHandler),
            conflictException.Message
        );

        await Results.Problem(
            detail: exception.Message,
            statusCode: StatusCodes.Status409Conflict
        ).ExecuteAsync(httpContext);

        return true;
    }
}
