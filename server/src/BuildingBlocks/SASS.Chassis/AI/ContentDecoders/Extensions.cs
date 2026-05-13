using System.Net.Mime;
using Microsoft.Extensions.DependencyInjection;

namespace SASS.Chassis.AI.ContentDecoders;

public static class Extensions
{
    public static IServiceCollection AddContentDecodersService(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IContentDecoder, PdfContentDecoder>(MediaTypeNames.Application.Pdf);
        services.AddKeyedSingleton<IContentDecoder, DocxContentDecoder>("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
        services.AddKeyedSingleton<IContentDecoder, TextContentDecoder>(MediaTypeNames.Text.Plain);
        services.AddKeyedSingleton<IContentDecoder, TextContentDecoder>(MediaTypeNames.Text.Markdown);

        return services;
    }
}
