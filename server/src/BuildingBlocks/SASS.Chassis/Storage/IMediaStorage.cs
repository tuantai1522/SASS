namespace SASS.Chassis.Storage;

/// <summary>
/// This interface will handle service related to upload file (CloudflareR2, Onpremise, ...)
/// </summary>
public interface IMediaStorage
{
    /// <summary>
    /// Method to get "PUT" presigned URl so client can upload to this.
    /// Returns object key and upload url.
    /// </summary>
    Task<(string Key, string Url)> GetPresignedUrl(string destinationPath, string fileName, string contentType);
    
    /// <summary>
    /// Method to get "GET" presigned URl so client view this file
    /// </summary>
    Task<string> GetPresignedUrl(string key);
}
