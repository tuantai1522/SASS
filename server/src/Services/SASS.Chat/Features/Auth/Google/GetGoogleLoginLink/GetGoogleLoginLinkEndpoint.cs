using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SASS.Chat.Features.Auth.Google.GetGoogleLoginLink;

public sealed class GetGoogleLoginLinkEndpoint : IEndpoint<Ok<GetGoogleLoginLinkResponse>, ISender>
{
    public async Task<Ok<GetGoogleLoginLinkResponse>> HandleAsync(ISender sender, CancellationToken cancellationToken = default)
    {
        var query = new GetGoogleLoginLinkEndpointQuery();

        var result = await sender.Send(query, cancellationToken);
        
        return TypedResults.Ok(result);
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/google/link", HandleAsync)
            .Produces<GetGoogleLoginLinkResponse>()
            .WithTags(nameof(User))
            .WithName(nameof(GetGoogleLoginLinkEndpoint))
            .WithDescription("Get google login link")
            .MapToApiVersion(ApiVersions.V1);
    }
}

