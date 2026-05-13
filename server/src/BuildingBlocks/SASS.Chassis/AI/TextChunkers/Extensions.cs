using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection;

namespace SASS.Chassis.AI.TextChunkers;

public static class Extensions
{
    public static IServiceCollection AddChunkingServices(this IServiceCollection services)
    {
        services.AddKeyedSingleton<ITextChunker, DefaultTextChunker>(KeyedService.AnyKey);
        services.AddKeyedSingleton<ITextChunker, MarkdownTextChunker>(MediaTypeNames.Text.Markdown);

        return services;
    }
}
