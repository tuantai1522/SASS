using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.TaskStatuses.GetTaskStatuses;

internal sealed class GetTaskStatusesQueryHandler(
    ChatDbContext dbContext) : IRequestHandler<GetTaskStatusesQuery, IReadOnlyList<GetTaskStatusesResponse>>
{
    public async Task<IReadOnlyList<GetTaskStatusesResponse>> Handle(GetTaskStatusesQuery request, CancellationToken cancellationToken)
    {
        var response = await dbContext.TaskStatuses
            .OrderBy(i => i.Order)
            .Select(x => new GetTaskStatusesResponse(x.Id, x.Name, x.Order))
            .ToListAsync(cancellationToken);

        return response;
    }
}
