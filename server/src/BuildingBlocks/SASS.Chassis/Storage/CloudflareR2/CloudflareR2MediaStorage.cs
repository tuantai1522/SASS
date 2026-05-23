using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace SASS.Chassis.Storage.CloudflareR2;

public sealed class CloudflareR2MediaStorage(IOptions<CloudflareR2Options> cloudflareR2Options, IAmazonS3 amazonS3) : IMediaStorage
{
    public async Task<(string Key, string Url)> GetPresignedUrl(string destinationPath, string fileName, string contentType)
    {
        try
        {
            var ufileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            var key = $"{destinationPath}/{ufileName}";

            var request = new GetPreSignedUrlRequest
            {
                BucketName = cloudflareR2Options.Value.BucketName,
                Key = key,
                ContentType = contentType,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Metadata =
                {
                    ["file-name"] = fileName
                }
            };

            var url = await amazonS3.GetPreSignedURLAsync(request);
            return (key, url);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to generate Cloudflare R2 upload presigned URL.", ex);
        }
    }

    public async Task<string> GetPresignedUrl(string key)
    {
        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = cloudflareR2Options.Value.BucketName,
                Key = key,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            return await amazonS3.GetPreSignedURLAsync(request);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to generate Cloudflare R2 view presigned URL.", ex);
        }
    }

    public async Task<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await amazonS3.GetObjectAsync(new GetObjectRequest
            {
                BucketName = cloudflareR2Options.Value.BucketName,
                Key = key
            }, cancellationToken);

            return new OwnedStream(response.ResponseStream, response);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to open Cloudflare R2 object stream for key '{key}'.", ex);
        }
    }
}
