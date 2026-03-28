using SASS.Chat.Configurations;

namespace SASS.Chat.Infrastructure;

internal static class Extensions
{
    extension(IHostApplicationBuilder builder)
    {
        public void AddPersistenceServices()
        {
            var services = builder.Services;

            builder.AddPostgresDbContext<ChatDbContext>(
                Components.Database.Chat,
                _ =>
                {
                    services.AddRepositories(typeof(IChatApiMarker));
                }
            );
        }

        public void AddGoogleAuthentication()
        {
            var services = builder.Services;
            services.AddHttpClient("GoogleAuth");
        }
    }
}
