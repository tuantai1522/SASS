using SASS.Chassis.AI.ContentDecoders;
using SASS.Chassis.AI.Ingestion;
using SASS.Chassis.AI.Search;
using SASS.Chat.Infrastructure.Ingestion;
using SASS.Chassis.AI.Extensions;
using SASS.Chassis.AI.TextChunkers;
using SASS.Chassis.AI.Token;
using SASS.Chat.Configurations;

namespace SASS.Chat.Infrastructure;

internal static class Extensions
{
    extension(IHostApplicationBuilder builder)
    {
        public void AddPersistenceServices()
        {
            builder.AddPostgresDbContext<ChatDbContext>(Components.Database.Chat);

            builder.Services.AddQdrantCollection<Guid, TextSnippet>(TextSnippet.CollectionName);
        }

        public void AddGoogleAuthentication()
        {
            var services = builder.Services;
            services.AddHttpClient(GoogleAuthOptions.SectionName);
        }

        public void AddAI()
        {
            var services = builder.Services;

            builder.AddAIServices(builder.Configuration);
            
            services.AddScoped<IIngestionSource<FileDataIngestion>, FileDataIngestor>();

            services
                .AddTokenServices()
                .AddChunkingServices()
                .AddContentDecodersService()
                .AddHybridSearch();
        }
    }
}
