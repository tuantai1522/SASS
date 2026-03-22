using SASS.Chat.Infrastructure;

namespace SASS.Chat.Extensions;

public static class Extensions
{
    public static IHostApplicationBuilder AddPersistenceServices(this IHostApplicationBuilder builder)
    {
        builder.AddPostgresDbContext<ChatDbContext>(Components.Database.Chat, excludeDefaultInterceptors: true);
        return builder;
    }
}
