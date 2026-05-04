using MediatR;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.CreateConversation;

internal sealed class CreateConversationCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider) : IRequestHandler<CreateConversationCommand, IdResult>
{
    public async Task<IdResult> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = Conversation.Create(userProvider.UserId, request.Name);

        await dbContext.Conversations.AddAsync(conversation, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(conversation.Id);
    }
}
