using SASS.Chat.Configurations;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddApiDocumentationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScalarApiDocumentation<ChatAppSettings>();
        return builder;
    }

    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddVersioning();
        builder.Services.AddEndpoints(typeof(IChatApiMarker));
        
        
        // Add exception handlers
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(IChatApiMarker).Assembly);

            config.ApplyLoggingBehavior();
            
            config.ApplyValidationBehavior();
        });        
        // Add database configuration
        builder.AddPersistenceServices();
                
        // Add google authentication
        builder.AddGoogleAuthentication(builder.Configuration);
        
        return builder;
    }

    public static WebApplication UseApiDocumentation(this WebApplication app)
    {
        app.MapScalarApiDocumentation<ChatAppSettings>();

        return app;
    }
}
