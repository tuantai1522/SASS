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

        public void AddGoogleAuthentication(IConfiguration configuration)
        {
            var services = builder.Services;
            services.AddHttpClient("GoogleAuth");

            services.AddOptions<GoogleAuthOptions>()
                .Bind(configuration.GetSection("GoogleAuth"))
                .Validate(o =>
                        !string.IsNullOrWhiteSpace(o.ClientId) &&
                        !string.IsNullOrWhiteSpace(o.ClientSecret) &&
                        !string.IsNullOrWhiteSpace(o.RedirectUri) &&
                        !string.IsNullOrWhiteSpace(o.Scope) &&
                        !string.IsNullOrWhiteSpace(o.GoogleUrl) &&
                        !string.IsNullOrWhiteSpace(o.GoogleAuthTokenEndpoint) &&
                        !string.IsNullOrWhiteSpace(o.GoogleContactInfoEndpoint),
                    "GoogleAuthOptions is invalid")
                .ValidateOnStart();
        }
    }
}
