using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SASS.Chassis.CQRS.Pipelines;

namespace SASS.Chassis.CQRS;

public static class Extensions
{
    extension(MediatRServiceConfiguration service)
    {
        public MediatRServiceConfiguration ApplyLoggingBehavior()
        {
            service.AddOpenBehavior(typeof(LoggingBehavior<,>));
            return service;
        }

        public MediatRServiceConfiguration ApplyValidationBehavior()
        {
            service.AddOpenBehavior(typeof(ValidationBehavior<,>));
            return service;
        }
    }
}
