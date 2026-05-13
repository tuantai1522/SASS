using FluentValidation;
using Microsoft.Extensions.Options;
using Qdrant.Client;
using SASS.Chassis.AI.Settings;
using SASS.Chassis.Security.Extensions;
using SASS.Chassis.Storage.Extensions;
using SASS.Chassis.WebStorages.Extensions;
using SASS.Chat.Configurations;
using SASS.Chat.Infrastructure;
using SASS.Chat.Realtime;

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

        // Configure AI
        builder.AddAI();

        // Add real-time SignalR service
        builder.AddRealtimeServices();

        // Configure FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<IChatApiMarker>(includeInternalTypes: true);

        // Add google authentication
        builder.AddGoogleAuthentication();

        builder.AddMediaStorage();

        builder.AddWebStorage();
    }

    public static WebApplication UseApiDocumentation(this WebApplication app)
    {
        app.MapScalarApiDocumentation<ChatAppSettings>();

        return app;
    }

    private static void AddOptions(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddOptions<SystemOptions>()
            .Bind(configuration.GetSection(SystemOptions.SectionName))
            .Validate(x => !string.IsNullOrWhiteSpace(x.DefaultConversationName), "Can't validate SystemOptions")
            .ValidateOnStart();

        builder.Services.AddOptions<GoogleAuthOptions>()
            .Bind(configuration.GetSection(GoogleAuthOptions.SectionName))
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

        builder.Services.AddOptions<QdrantOptions>()
            .Bind(configuration.GetSection(QdrantOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.Host), "Qdrant host is required")
            .Validate(o => o.Port > 0, "Qdrant port must be greater than zero")
            .ValidateOnStart();

        builder.Services.AddSingleton(sp =>
        {
            var qdrantOptions = sp.GetRequiredService<IOptions<QdrantOptions>>().Value;

            return new QdrantClient(
                qdrantOptions.Host,
                qdrantOptions.Port,
                qdrantOptions.Https,
                qdrantOptions.ApiKey
            );
        });
        
        builder.Services.AddOptions<ChunkingAIOptions>()
            .Bind(configuration.GetSection(ChunkingAIOptions.SectionName))
            .Validate(o => o.MaxTokensPerLine > 0, "MaxTokensPerLine port must be greater than zero")
            .Validate(o => o.MaxTokensPerParagraph > 0, "MaxTokensPerParagraph port must be greater than zero")
            .Validate(o => o.OverlapTokens > 0, "OverlapTokens port must be greater than zero")
            .ValidateOnStart();

        builder.Services.AddSingleton(sp =>
        {
            var qdrantOptions = sp.GetRequiredService<IOptions<QdrantOptions>>().Value;

            return new QdrantClient(
                qdrantOptions.Host,
                qdrantOptions.Port,
                qdrantOptions.Https,
                qdrantOptions.ApiKey
            );
        });
    }

    private static void AddDefaultApiDocumentation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScalarApiDocumentation<ChatAppSettings>();
    }

}
