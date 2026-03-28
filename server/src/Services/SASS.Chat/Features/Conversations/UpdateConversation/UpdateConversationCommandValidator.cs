using FluentValidation;

namespace SASS.Chat.Features.Conversations.UpdateConversation;

internal sealed class UpdateConversationCommandValidator : AbstractValidator<UpdateConversationCommand>
{
    public UpdateConversationCommandValidator()
    {
        RuleFor(x => x.ConversationId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}
