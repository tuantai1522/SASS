using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Files.GetPresignedDownloadUrl;

public sealed class GetPresignedDownloadUrlEndpoint
    : IEndpoint<Ok<GetPresignedDownloadUrlResponse>, Guid, Guid, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("conversations/{conversationId:guid}/files/{fileId:guid}/presigned-download", HandleAsync)
            .WithTags(nameof(File))
            .WithName(nameof(GetPresignedDownloadUrlEndpoint))
            .WithDescription("Get presigned download url for file")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<GetPresignedDownloadUrlResponse>();
    }

    public async Task<Ok<GetPresignedDownloadUrlResponse>> HandleAsync(
        [FromRoute] Guid conversationId,
        [FromRoute] Guid fileId,
        ISender sender,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sender.Send(new GetPresignedDownloadUrlQuery(conversationId, fileId), cancellationToken);
        return TypedResults.Ok(response);
    }
}
