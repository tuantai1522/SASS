using MediatR;

namespace SASS.Chat.Features.Auth.RenewAccessToken;

public sealed record RenewAccessTokenCommand : IRequest<RenewAccessTokenResponse>;
