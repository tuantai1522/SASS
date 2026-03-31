using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SASS.Chassis.Cors;

public static class Extension
{
    public static IHostApplicationBuilder AddDefaultCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? [])
                    .AllowAnyMethod() // Allow all method (GET, POST, PUT, DELETE, etc.)
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition") // Header
                    .AllowCredentials();
            });
        });
        return builder;
    }
    
    public static void UseDefaultCors(this WebApplication app)
    {
        app.UseCors();
    }
}
        