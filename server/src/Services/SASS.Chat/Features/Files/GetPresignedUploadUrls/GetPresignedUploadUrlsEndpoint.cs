using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Files.GetPresignedUploadUrls;

public sealed class GetPresignedUploadUrlsEndpoint
    : IEndpoint<Ok<GetPresignedUploadUrlsResponse>, IReadOnlyList<GetPresignedUploadFileItems>, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("files/presigned-upload", HandleAsync)
            .WithTags(nameof(File))
            .WithName(nameof(GetPresignedUploadUrlsEndpoint))
            .WithDescription("Generate presigned upload urls for multiple files")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<GetPresignedUploadUrlsResponse>();
    }

    public async Task<Ok<GetPresignedUploadUrlsResponse>> HandleAsync(
        [FromBody] IReadOnlyList<GetPresignedUploadFileItems> request,
        ISender sender,
        CancellationToken cancellationToken = default
    )
    {
        var command = new GetPresignedUploadUrlsCommand(request);
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }
}
