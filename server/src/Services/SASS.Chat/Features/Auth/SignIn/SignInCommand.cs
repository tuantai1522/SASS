using MediatR;

namespace SASS.Chat.Features.Auth.SignIn;

public sealed record SignInCommand(string Email, string Password) : IRequest<SignInResponse>;