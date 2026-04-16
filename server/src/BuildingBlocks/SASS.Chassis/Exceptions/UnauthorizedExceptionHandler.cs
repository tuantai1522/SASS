using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SASS.Chassis.Exceptions;

public sealed class UnauthorizedException(string message) : Exception(message);

/// <summary>
/// Todo: To check PerRequestLogBuffer
/// This exception will catch unauthorized exception
/// </summary>
public sealed class UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not UnauthorizedException unauthorizedException)
        {
            return false;
        }

        logger.LogWarning(
            exception,
            "[{Handler}] Unauthorized exception occurred: {Message}",
            nameof(UnauthorizedExceptionHandler),
            unauthorizedException.Message
        );

        await Results.Problem(
            detail: exception.Message,
            statusCode: StatusCodes.Status401Unauthorized
        ).ExecuteAsync(httpContext);

        return true;
    }
}
