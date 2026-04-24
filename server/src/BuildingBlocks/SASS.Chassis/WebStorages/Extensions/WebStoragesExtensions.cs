using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SASS.Chassis.WebStorages.Extensions;

public static class WebStoragesExtensions
{
    public static void AddWebStorage(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        
        services
            .AddScoped<ICookieService, CookieService>();
    }
}
