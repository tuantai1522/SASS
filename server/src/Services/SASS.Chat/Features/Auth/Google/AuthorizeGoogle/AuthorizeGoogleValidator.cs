using FluentValidation;

namespace SASS.Chat.Features.Auth.Google.AuthorizeGoogle;

internal sealed class AuthorizeGoogleValidator : AbstractValidator<AuthorizeGoogleQuery>
{
    public AuthorizeGoogleValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("Code is required. It can not be empty.");
    }
}