using FluentValidation;

namespace SASS.Chat.Features.Conversations.GetMessages;

internal sealed class GetMessagesQueryValidator : AbstractValidator<GetMessagesQuery>
{
    public GetMessagesQueryValidator()
    {
        RuleFor(x => x.CursorRequest.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("Cursor request limit must be greater than or equal to 100.");
        
        RuleFor(x => x.ConversationId).NotEmpty();
    }
}
