using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Conversations.GetConversationById;

internal sealed class GetConversationByIdQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider
) : IRequestHandler<GetConversationByIdQuery, GetConversationByIdResponse>
{
    public async Task<GetConversationByIdResponse> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await dbContext.Conversations
            .AsNoTracking()
            .Where(x => x.Id == request.ConversationId && x.UserId == userProvider.UserId)
            .Select(x => new GetConversationByIdResponse(x.Id, x.Name))
            .FirstOrDefaultAsync(
                cancellationToken
            );

        Guard.Against.NotFound(response, request.ConversationId);

        return response;
    }
}
