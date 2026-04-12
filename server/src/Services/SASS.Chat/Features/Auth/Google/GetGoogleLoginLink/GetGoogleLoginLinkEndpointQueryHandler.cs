using MediatR;
using Microsoft.Extensions.Options;
using SASS.Chat.Configurations;
using Task = System.Threading.Tasks.Task;

namespace SASS.Chat.Features.Auth.Google.GetGoogleLoginLink;

internal sealed class GetGoogleLoginLinkEndpointQueryHandler(IOptions<GoogleAuthOptions> options) 
    : IRequestHandler<GetGoogleLoginLinkEndpointQuery, GetGoogleLoginLinkResponse>
{
    public Task<GetGoogleLoginLinkResponse> Handle(GetGoogleLoginLinkEndpointQuery request, CancellationToken cancellationToken)
    {
        var settings = options.Value;

        var clientId = settings.ClientId;
        var redirectUri = settings.RedirectUri;
        var scope = settings.Scope;
        
        var loginUrl = settings.GoogleUrl
            .Replace("{clientId}", clientId)
            .Replace("{redirectUri}", Uri.EscapeDataString(redirectUri))
            .Replace("{scope}", Uri.EscapeDataString(scope));

        return Task.FromResult(new GetGoogleLoginLinkResponse(loginUrl));
    }
}
