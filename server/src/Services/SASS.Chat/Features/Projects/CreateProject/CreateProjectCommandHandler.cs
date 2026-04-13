using MediatR;
using Microsoft.EntityFrameworkCore;
using SASS.Chassis.Security.UserRetrieval;
using SASS.Chat.Infrastructure;

namespace SASS.Chat.Features.Projects.CreateProject;

internal sealed class CreateProjectCommandHandler(
    ChatDbContext dbContext,
    IUserProvider userProvider)
    : IRequestHandler<CreateProjectCommand, IdResult>
{
    public async Task<IdResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var hasDuplicateCode = await dbContext.Projects
            .AnyAsync(x => x.OwnerId == userProvider.UserId
                           && x.Code == request.Code
                           && !x.IsDeleted,
                cancellationToken);

        if (hasDuplicateCode)
        {
            throw new InvalidOperationException("Project code already exists for current owner.");
        }

        var memberIds = request.MemberIds;
        var leaderIds = request.LeaderIds;

        var participantIds = memberIds
            .Concat(leaderIds)
            .Append(userProvider.UserId)
            .Distinct()
            .ToArray();

        var existingParticipantsCount = await dbContext.Users
            .CountAsync(x => participantIds.Contains(x.Id), cancellationToken);

        if (existingParticipantsCount != participantIds.Length)
        {
            throw new NotFoundException("One or more project participants were not found.");
        }

        // 1. Add from request
        var project = Project.Create(userProvider.UserId, request.Code, request.Title, request.Description, request.MemberIds, request.LeaderIds);

        // 2. Add current user as leader
        project.AddMembers([ProjectMember.Create(project.Id, userProvider.UserId,  ProjectMemberRole.Leader)]);

        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(project.Id);
    }
}
