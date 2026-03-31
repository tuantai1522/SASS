namespace SASS.Chassis.Storage.CloudflareR2;

public sealed class CloudflareR2Options
{
    public required string AccessKeyId { get; init; }
        
    public required string SecretAccessKey { get; init; }
        
    public required string BucketName { get; init; }
}
