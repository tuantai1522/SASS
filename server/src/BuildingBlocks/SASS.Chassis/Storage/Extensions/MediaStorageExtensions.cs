using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SASS.Chassis.Storage.CloudflareR2;

namespace SASS.Chassis.Storage.Extensions;

public static class MediaStorageExtensions
{
    public static void AddMediaStorage(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddOptions<MediaStorageOptions>()
            .Bind(configuration.GetSection(nameof(MediaStorageOptions)))
            .Validate(o => Enum.IsDefined(typeof(MediaStorageProvider), o.Provider), "Invalid MediaStorageProvider value")
            .ValidateOnStart();

        builder.AddCloudflareR2(builder.Configuration);
        builder.Services.AddKeyedScoped<IMediaStorage, CloudflareR2MediaStorage>(MediaStorageProvider.CloudflareR2);

        // Todo: To add other services
        // builder.Services.AddKeyedScoped<IMediaStorage, FirebaseFileExplorer>(MediaStorageProvider.Firebase);
    }
    
    private static void AddCloudflareR2(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddOptions<CloudflareR2Options>()
            .Bind(configuration.GetSection(CloudflareR2Options.SectionName))
            .Validate(o =>
                    new[]
                    {
                        o.AccessKeyId,
                        o.SecretAccessKey,
                        o.BucketName,
                        o.ServiceUrl
                    }.All(v => !string.IsNullOrWhiteSpace(v)),
                "CloudflareR2 is invalid")
            .ValidateOnStart();
        
        builder.Services.AddSingleton<IAmazonS3>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<CloudflareR2Options>>().Value;

            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceUrl,
                ForcePathStyle = true,
                AuthenticationRegion = "auto"
            };

            return new AmazonS3Client(options.AccessKeyId, options.SecretAccessKey, config);
        });
    }
}
