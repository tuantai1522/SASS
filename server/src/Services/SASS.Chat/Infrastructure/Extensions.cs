using SASS.Chat.Configurations;

namespace SASS.Chat.Infrastructure;

internal static class Extensions
{
    extension(IHostApplicationBuilder builder)
    {
        public void AddPersistenceServices()
        {
            builder.AddPostgresDbContext<ChatDbContext>(Components.Database.Chat);
        }

        public void AddGoogleAuthentication()
        {
            var services = builder.Services;
            services.AddHttpClient("GoogleAuth");
        }
    }
}
