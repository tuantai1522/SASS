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

    public static IHostApplicationBuilder AddPersistenceServices(this IHostApplicationBuilder builder)
    {
        builder.AddPostgresDbContext<ChatDbContext>(Components.Database.Chat, excludeDefaultInterceptors: true);
        return builder;
    }

    public static WebApplication UseApiDocumentation(this WebApplication app)
    {
        app.MapScalarApiDocumentation<ChatAppSettings>();

        return app;
    }
}
