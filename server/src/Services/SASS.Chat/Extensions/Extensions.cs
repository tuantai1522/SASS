using FluentValidation;
using SASS.Chassis.Security.Extensions;
using SASS.Chassis.Storage.Extensions;
using SASS.Chat.Configurations;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        // Add all options of system
        builder.AddOptions(builder.Configuration);
        
        builder.AddDefaultCors();

        builder.AddDefaultApiDocumentation();

        builder.Services.AddVersioning();
        builder.Services.AddEndpoints(typeof(IChatApiMarker));
        
        builder.AddDefaultAuthentication();
        builder.AddPasswordHashingService();
        builder.Services.AddAuthorization();
        
        // Add exception handlers
        // Exception related to bad request (404)
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();

        // Exception related to not found (400)
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();

        // Exception related to conflict (409)
        builder.Services.AddExceptionHandler<ConflictExceptionHandler>();

        // Exception related to unauthorized (401)
        builder.Services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        
        // Global exception
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
                
        // Configure FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<IChatApiMarker>(includeInternalTypes: true);
        
        // Add google authentication
        builder.AddGoogleAuthentication();
        
        builder.AddMediaStorage();
    }

    public static WebApplication UseApiDocumentation(this WebApplication app)
    {
        app.MapScalarApiDocumentation<ChatAppSettings>();

        return app;
    }

    private static void AddOptions(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddOptions<SystemOptions>()
            .Bind(builder.Configuration.GetSection("SystemOptions"))
            .Validate(x => !string.IsNullOrWhiteSpace(x.DefaultConversationName), "Can't validate SystemOptions")
            .ValidateOnStart();
        
        builder.Services.AddOptions<GoogleAuthOptions>()
            .Bind(configuration.GetSection("GoogleAuth"))
            .Validate(o =>
                    new[]
                    {
                        o.ClientId,
                        o.ClientSecret,
                        o.RedirectUri,
                        o.Scope,
                        o.GoogleUrl,
                        o.GoogleAuthTokenEndpoint,
                        o.GoogleContactInfoEndpoint
                    }.All(v => !string.IsNullOrWhiteSpace(v)), 
                "GoogleAuthOptions is invalid")
            .ValidateOnStart();
    }

    private static void AddDefaultApiDocumentation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScalarApiDocumentation<ChatAppSettings>();
    }
    
}
