using MediatR;
using System.Text.Json.Serialization;

namespace SASS.Chat.Features.Projects.UpdateProject;

public sealed record UpdateProjectCommand : IRequest<IdResult>
{
    public Guid ProjectId { get; init; }

    /// <summary>
    /// On client, they will send "Title", "Description", ...
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<UpdateProjectKey>))]
    public UpdateProjectKey Key { get; init; }

    public string Value { get; init; } = null!;
}
