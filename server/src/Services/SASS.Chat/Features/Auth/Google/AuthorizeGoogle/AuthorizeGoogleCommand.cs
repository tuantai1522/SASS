using MediatR;

namespace SASS.Chat.Features.Auth.Google.AuthorizeGoogle;

public sealed record AuthorizeGoogleCommand(string Code) : IRequest<AuthorizeGoogleResponse>;
