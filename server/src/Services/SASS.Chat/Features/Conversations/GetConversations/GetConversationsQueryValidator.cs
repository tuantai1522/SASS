using FluentValidation;

namespace SASS.Chat.Features.Conversations.GetConversations;

internal sealed class GetConversationsQueryValidator : AbstractValidator<GetConversationsQuery>
{
    public GetConversationsQueryValidator()
    {
        RuleFor(x => x.CursorRequest.Limit)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("Cursor request limit must be greater than or equal to 100.");
    }
}
