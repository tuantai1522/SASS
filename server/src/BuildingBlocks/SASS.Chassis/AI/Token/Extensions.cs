using Microsoft.Extensions.DependencyInjection;

namespace SASS.Chassis.AI.Token;

public static class Extensions
{
    public static IServiceCollection AddTokenServices(this IServiceCollection services)
    {
        services.AddSingleton<ITokenCounter, SharpTokenCounter>();

        return services;
    }
}
