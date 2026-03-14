using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using SASS.Constants.Core;

namespace SASS.Chassis.Endpoints;

public static class Extension
{
    /// <summary>
    /// To define versioning for API (current is v1)
    /// </summary>
    public static void AddVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersions.V1;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}
