namespace SASS.Chassis.Storage.CloudflareR2;

public sealed class CloudflareR2Options
{
    public const string SectionName = "CloudflareR2";
    
    public required string AccessKeyId { get; init; }
        
    public required string SecretAccessKey { get; init; }
        
    public required string BucketName { get; init; }
    
    public required string ServiceUrl { get; init; }
}
