using MediatR;

namespace SASS.Chat.Features.Projects.CreateProject;

public sealed record CreateProjectCommand(
    string Code,
    string Title,
    string? Description,
    IReadOnlyList<Guid> MemberIds,
    IReadOnlyList<Guid> LeaderIds) : IRequest<IdResult>;
