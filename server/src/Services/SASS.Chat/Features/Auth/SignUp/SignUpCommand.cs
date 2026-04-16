using MediatR;

namespace SASS.Chat.Features.Auth.SignUp;

public sealed record SignUpCommand(string DisplayName, string Email, string Password) : IRequest<IdResult>;
