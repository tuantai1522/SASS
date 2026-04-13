using MediatR;
using System.Text.Json.Serialization;

namespace SASS.Chat.Features.Projects.UpdateProjectMember;

public sealed record UpdateProjectMemberCommand : IRequest<IdResult>
{
    public Guid ProjectId { get; init; }

    public Guid UserId { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter<ProjectMemberRole>))]
    public ProjectMemberRole Role { get; init; }
}
