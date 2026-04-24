using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chassis.Utilities.Guards;
using SASS.Chat.Features.Projects.Utils;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.GetProjectById;

internal sealed class GetProjectByIdQueryHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
{
    public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = userProvider.UserId;

        var response = await dbContext.Projects
            .AsNoTracking()
            .Where(x => x.Id == request.ProjectId && x.Members.Any(m => m.UserId == userId))
            .Select(x => new GetProjectByIdResponse
            {
                Id = x.Id,
                Code = x.Code,
                Title = x.Title,
                Description = x.Description,
                CreatedAt = x.CreatedAt,
                OwnerId = x.OwnerId,
                Role = x.Members
                    .Where(m => m.UserId == userId)
                    .Select(m => m.Role.ToString())
                    .FirstOrDefault() ?? nameof(ProjectMemberRole.Member),
                Progress = ProjectProgressCalculator.Calculate(x.Tasks.Count(t => !t.IsDeleted && t.Status.Name == nameof(TaskStatusKey.Done)), x.Tasks.Count(t => !t.IsDeleted))
            })
            .FirstOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(response, request.ProjectId);

        return response;
    }
}
