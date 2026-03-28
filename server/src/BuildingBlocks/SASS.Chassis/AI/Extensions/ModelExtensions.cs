using System.ClientModel;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using SASS.Chassis.AI.Settings;

namespace SASS.Chassis.AI.Extensions;

public static class ModelExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder AddAIServices(IConfiguration configuration)
        {
            var services = builder.Services;
            
            services.AddOptions<OpenRouterAIOptions>()
                .Bind(configuration.GetSection(nameof(OpenRouterAIOptions)))
                .Validate(o =>
                        new[]
                        {
                            o.Url,
                            o.ApiKey,
                            o.ChatModelId,
                            o.EmbeddingModelId
                        }.All(v => !string.IsNullOrWhiteSpace(v)),
                    "OpenRouterAIOptions is invalid")
                .ValidateOnStart();

            services.AddSingleton<IChatClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<OpenRouterAIOptions>>().Value;

                return new ChatClient(
                    model: options.ChatModelId,
                    credential: new ApiKeyCredential(options.ApiKey),
                    options: new OpenAIClientOptions
                    {
                        Endpoint = new Uri(options.Url)
                    }
                ).AsIChatClient();
            });

            services.AddSingleton<IEmbeddingGenerator<string, Embedding<float>>>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<OpenRouterAIOptions>>().Value;

                return new OpenAI.Embeddings.EmbeddingClient(
                    model: options.EmbeddingModelId,
                    credential: new ApiKeyCredential(options.ApiKey),
                    options: new OpenAIClientOptions
                    {
                        Endpoint = new Uri(options.Url)
                    }
                ).AsIEmbeddingGenerator();
            });

            return builder;
        }
    }
}
