using MediatR;

namespace SASS.Chat.Features.Auth.Google.GetGoogleLoginLink;

public sealed record GetGoogleLoginLinkEndpointQuery : IRequest<GetGoogleLoginLinkResponse>;
