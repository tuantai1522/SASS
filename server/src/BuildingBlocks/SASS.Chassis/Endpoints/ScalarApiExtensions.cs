using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using SASS.Chassis.Endpoints.Settings;
using Scalar.AspNetCore;

namespace SASS.Chassis.Endpoints;

public static class ScalarApiExtensions
{
    public static IServiceCollection AddScalarApiDocumentation<TOptions>(this IServiceCollection services)
        where TOptions : AppSettings, new()
    {
        var scalarOptions = new TOptions();

        services.AddOpenApi(
            scalarOptions.DocumentName,
            options =>
            {
                options.AddDocumentTransformer(
                    (document, _, _) =>
                    {
                        document.Info = new OpenApiInfo
                        {
                            Title = scalarOptions.Title,
                            Description = scalarOptions.Description,
                            Version = scalarOptions.OpenApiVersion,
                            Contact = new OpenApiContact
                            {
                                Name = scalarOptions.Contact.Name,
                            }
                        };

                        if (scalarOptions.EnableJwtBearerSecurity)
                        {
                            document.Components ??= new OpenApiComponents();
                            document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
                            document.Components.SecuritySchemes[scalarOptions.JwtSecuritySchemeName] = new OpenApiSecurityScheme
                            {
                                Name = "Authorization",
                                Type = SecuritySchemeType.Http,
                                Scheme = "bearer",
                                BearerFormat = "JWT",
                                In = ParameterLocation.Header,
                                Description = scalarOptions.JwtSecurityDescription
                            };

                            document.Security =
                            [
                                new OpenApiSecurityRequirement
                                {
                                    [
                                        new OpenApiSecuritySchemeReference(scalarOptions.JwtSecuritySchemeName, null, null)
                                    ] = []
                                }
                            ];
                        }

                        return Task.CompletedTask;
                    }
                );
            }
        );

        return services;
    }

    public static WebApplication MapScalarApiDocumentation<TOptions>(
        this WebApplication app,
        Action<ScalarOptions>? configure = null
    )
        where TOptions : AppSettings, new()
    {
        var scalarOptions = new TOptions();

        if (!scalarOptions.Enabled)
        {
            return app;
        }

        if (scalarOptions.DevelopmentOnly && !app.Environment.IsDevelopment())
        {
            return app;
        }

        app.MapOpenApi(scalarOptions.OpenApiRoutePattern);

        if (string.IsNullOrWhiteSpace(scalarOptions.EndpointPrefix))
        {
            app.MapScalarApiReference(x => ConfigureScalar(x, scalarOptions, configure));
            return app;
        }

        app.MapScalarApiReference(
            scalarOptions.EndpointPrefix,
            x => ConfigureScalar(x, scalarOptions, configure)
        );

        return app;
    }

    private static void ConfigureScalar(ScalarOptions options, AppSettings appSettings, Action<ScalarOptions>? configure)
    {
        options.WithTitle(appSettings.Title);
        options.OpenApiRoutePattern = appSettings.OpenApiRoutePattern;

        if (appSettings.DocumentNames.Count != 0)
        {
            options.AddDocuments(appSettings.DocumentNames);
        }

        if (appSettings.EnableJwtBearerSecurity)
        {
            options.AddHttpAuthentication(
                appSettings.JwtSecuritySchemeName,
                x => x.WithToken(appSettings.DefaultJwtToken ?? string.Empty)
            );
            options.AddPreferredSecuritySchemes([appSettings.JwtSecuritySchemeName]);
        }

        configure?.Invoke(options);
    }
}
