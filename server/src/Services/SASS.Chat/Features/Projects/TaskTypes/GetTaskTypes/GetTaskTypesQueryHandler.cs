using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.TaskTypes.GetTaskTypes;

internal sealed class GetTaskTypesQueryHandler(
    ChatDbContext dbContext) : IRequestHandler<GetTaskTypesQuery, IReadOnlyList<GetTaskTypesResponse>>
{
    public async Task<IReadOnlyList<GetTaskTypesResponse>> Handle(GetTaskTypesQuery request, CancellationToken cancellationToken)
    {
        var response = await dbContext.TaskTypes
            .OrderBy(i => i.Order)
            .Select(x => new GetTaskTypesResponse(x.Id, x.Name, x.Key, x.ColorToken, x.IconKey, x.Order))
            .ToListAsync(cancellationToken);

        return response;
    }
}
