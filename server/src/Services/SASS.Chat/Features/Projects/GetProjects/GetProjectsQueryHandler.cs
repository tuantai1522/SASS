using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.GetProjects;

internal sealed class GetProjectsQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<GetProjectsQuery, IReadOnlyList<GetProjectsResponse>>
{
    public async Task<IReadOnlyList<GetProjectsResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Projects
            .AsNoTracking()
            .Where(x => x.Members.Any(m => m.UserId == userProvider.UserId))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new GetProjectsResponse(x.Id, x.Code, x.Title))
            .ToListAsync(cancellationToken);
    }
}
