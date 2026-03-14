using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SASS.Chassis.CQRS.Pipelines;

internal sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger ) : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        const string behavior = nameof(ValidationBehavior<,>);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation(
                "[{Behavior}] handle request={RequestData} and response={ResponseData}",
                behavior,
                request.GetType().Name,
                typeof(TResponse).Name
            );
        }

        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResult = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var errors = validationResult
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .Select(failure => new ValidationFailure(failure.PropertyName, failure.ErrorMessage))
            .ToList();

        if (errors.Count == 0)
        {
            return await next(cancellationToken);
        }
        
        logger.LogWarning(
            "[{Behavior}] Validation failed for {Request}. ErrorCount={ErrorCount}",
            behavior,
            typeof(TRequest).Name,
            errors.Count
        );

        throw new ValidationException(errors);

    }
}
