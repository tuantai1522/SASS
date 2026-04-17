using MediatR;

namespace SASS.Chat.Features.Users.GetMe;

public sealed record GetMeQuery : IRequest<GetMeResponse>;
