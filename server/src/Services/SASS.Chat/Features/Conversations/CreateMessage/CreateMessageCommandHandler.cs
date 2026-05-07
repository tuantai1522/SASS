using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.CreateMessage;

internal sealed class CreateMessageCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider) : IRequestHandler<CreateMessageCommand, IdResult>
{
    public async Task<IdResult> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var senderId = userProvider.UserId;

        var conversation = await dbContext.Conversations
            .FirstOrDefaultAsync(
                x => x.Id == request.ConversationId &&
                     x.ConversationMembers.Any(cm => cm.MemberId == senderId && !cm.IsDeleted),
                cancellationToken);

        Guard.Against.NotFound(conversation, request.ConversationId);

        var message = Message.Create(
            request.ConversationId,
            request.Content,
            senderId);

        conversation.AddMessage(message);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(message.Id);
    }
}
