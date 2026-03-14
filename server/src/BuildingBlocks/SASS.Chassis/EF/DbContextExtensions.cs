using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SASS.SharedKernel.EventPublishers;

namespace SASS.Chassis.EF;

public static class DbContextExtensions
{
    /// <summary>
    /// Add DI for database service
    /// </summary>
    /// <param name="builder">Get instance services</param>
    /// <param name="connectionString">Connection string to connect database</param>
    /// <param name="action">Action to add other services beside DbContext</param>
    /// <param name="excludeDefaultInterceptors">To add default interceptors or not</param>
    /// <typeparam name="TDbContext"></typeparam>
    public static void AddPostgresDbContext<TDbContext>(
        this IHostApplicationBuilder builder,
        string connectionString,
        Action<IHostApplicationBuilder>? action = null,
        bool excludeDefaultInterceptors = false
    )
        where TDbContext : DbContext
    {
        var services = builder.Services;

        if (!excludeDefaultInterceptors)
        {
            services.AddScoped<IInterceptor, QueryPerformanceInterceptor>();
            services.AddScoped<IInterceptor, EventDispatchInterceptor>();
            services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();
        }

        services.AddDbContext<TDbContext>(
            (sp, options) =>
            {
                options
                    .UseNpgsql(builder.Configuration.GetConnectionString(connectionString))
                    .UseSnakeCaseNamingConvention();
 

                var interceptors = sp.GetServices<IInterceptor>().ToArray();

                if (interceptors.Length != 0)
                {
                    options.AddInterceptors(interceptors);
                }
            }
        );

        action?.Invoke(builder);
    }
}
