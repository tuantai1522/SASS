using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SASS.Chassis.Exceptions;

/// <summary>
/// Todo: To check PerRequestLogBuffer
/// This exception will catch validation exception
/// </summary>
public sealed class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger ) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        logger.LogError(
            validationException,
            "[{Handler}] Exception occurred: {Message}",
            nameof(ValidationExceptionHandler),
            validationException.Message
        );

        var errors = validationException
            .Errors.GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        await TypedResults
            .ValidationProblem(errors, title: "One or more validation errors occurred.")
            .ExecuteAsync(httpContext);

        return true;
    }
}
