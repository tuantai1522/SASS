using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SASS.Chassis.CQRS.Pipelines;

namespace SASS.Chassis.CQRS;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection ApplyLoggingBehavior()
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return services;
        }

        public IServiceCollection ApplyValidationBehavior()
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
