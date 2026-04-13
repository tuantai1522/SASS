using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.UpdateConversation;

internal sealed class UpdateConversationCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider
) : IRequestHandler<UpdateConversationCommand, IdResult>
{
    public async Task<IdResult> Handle(UpdateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await dbContext.Conversations
            .FirstOrDefaultAsync(
                x => x.Id == request.ConversationId && x.UserId == userProvider.UserId,
                cancellationToken
            );

        Guard.Against.NotFound(conversation, request.ConversationId);

        conversation.ChangeName(request.Name);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(conversation.Id);
    }
}
