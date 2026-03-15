using Microsoft.Extensions.DependencyInjection;

namespace SASS.Chassis.Repository;

public static class Extensions
{
    public static void AddRepositories(this IServiceCollection services, Type type)
    {
        services.Scan(scan =>
            scan.FromAssembliesOf(type)
                .AddClasses(classes => classes.AssignableTo(typeof(IRepository<>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }
}
