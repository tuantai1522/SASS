using Microsoft.Extensions.DependencyInjection;

namespace SASS.Chassis.AI.Search;

public static class Extensions
{
    public static void AddHybridSearch(this IServiceCollection services)
    {
        services.AddScoped<ISearch, HybridSearch>();
    }
}
