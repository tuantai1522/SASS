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
            throw new ConflictException("Project code already exists for current owner.");
        }

        // 1. Create project
        var project = Project.Create(userProvider.UserId, request.Code, request.Title, request.Description);

        // 2. Add current user as leader
        project.AddMembers([ProjectMember.Create(project.Id, userProvider.UserId, ProjectMemberRole.Leader)]);

        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new IdResult(project.Id);
    }
}
