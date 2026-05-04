using FluentValidation;

namespace SASS.Chat.Features.Conversations.CreateConversation;

internal sealed class CreateConversationCommandValidator : AbstractValidator<CreateConversationCommand>
{
    public CreateConversationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
