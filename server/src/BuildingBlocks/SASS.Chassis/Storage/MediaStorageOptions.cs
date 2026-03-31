namespace SASS.Chassis.Storage;

/// <summary>
/// This option defines which upload file service
/// </summary>
public sealed class MediaStorageOptions
{
    public MediaStorageProvider Provider { get; init; }
}