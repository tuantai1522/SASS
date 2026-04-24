using MediatR;

namespace SASS.Chat.Features.Auth.SignOut;

public sealed record SignOutCommand : IRequest<Unit>;
