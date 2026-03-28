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
    }
}
