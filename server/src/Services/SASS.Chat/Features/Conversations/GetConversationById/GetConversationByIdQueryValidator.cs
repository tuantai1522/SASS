using FluentValidation;

namespace SASS.Chat.Features.Conversations.GetConversationById;

internal sealed class GetConversationByIdQueryValidator : AbstractValidator<GetConversationByIdQuery>
{
    public GetConversationByIdQueryValidator()
    {
        RuleFor(x => x.ConversationId).NotEmpty();
    }
}
