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
            .Bind(configuration.GetSection(nameof(CloudflareR2Options)))
            .Validate(o =>
                    new[]
                    {
                        o.AccessKeyId,
                        o.BucketName,
                        o.SecretAccessKey
                    }.All(v => !string.IsNullOrWhiteSpace(v)),
                "JwtOptions is invalid")
            .ValidateOnStart();
        
        builder.Services.AddSingleton<IAmazonS3>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<CloudflareR2Options>>().Value;

            var config = new AmazonS3Config
            {
                ServiceURL = "https://3a3d8f07c684619db1a93ca07b262e29.r2.cloudflarestorage.com",
                ForcePathStyle = true,
                AuthenticationRegion = "auto"
            };

            return new AmazonS3Client(options.AccessKeyId, options.SecretAccessKey, config);
        });
    }
}
