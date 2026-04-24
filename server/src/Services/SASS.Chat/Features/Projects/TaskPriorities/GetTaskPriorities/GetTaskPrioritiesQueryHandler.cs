using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.TaskPriorities.GetTaskPriorities;

internal sealed class GetTaskPrioritiesQueryHandler(
    ChatDbContext dbContext) : IRequestHandler<GetTaskPrioritiesQuery, IReadOnlyList<GetTaskPrioritiesResponse>>
{
    public async Task<IReadOnlyList<GetTaskPrioritiesResponse>> Handle(GetTaskPrioritiesQuery request, CancellationToken cancellationToken)
    {
        var response = await dbContext.TaskPriorities
            .OrderBy(i => i.Order)
            .Select(x => new GetTaskPrioritiesResponse(x.Id, x.Name, x.Order))
            .ToListAsync(cancellationToken);

        return response;
    }
}
