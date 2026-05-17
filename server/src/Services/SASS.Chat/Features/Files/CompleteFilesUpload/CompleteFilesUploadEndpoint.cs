using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SASS.Chat.Features.Files.CompleteFilesUpload;

public sealed class CompleteFilesUploadEndpoint
    : IEndpoint<Ok<CompleteFilesUploadResponse>, CompleteFilesUploadRequest, ISender>
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("files/complete", HandleAsync)
            .WithTags(nameof(File))
            .WithName(nameof(CompleteFilesUploadEndpoint))
            .WithDescription("Mark uploaded files as completed for conversation")
            .MapToApiVersion(ApiVersions.V1)
            .RequireAuthorization()
            .Produces<CompleteFilesUploadResponse>();
    }

    public async Task<Ok<CompleteFilesUploadResponse>> HandleAsync(
        [FromBody] CompleteFilesUploadRequest request,
        ISender sender,
        CancellationToken cancellationToken = default
    )
    {
        var command = new CompleteFilesUploadCommand(request.FileIds);
        var result = await sender.Send(command, cancellationToken);
        return TypedResults.Ok(result);
    }
}
