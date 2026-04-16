using System.Text.Json.Serialization;
using MediatR;

namespace SASS.Chat.Features.Projects.UpdateProjectTask;

public sealed record UpdateProjectTaskCommand : IRequest<IdResult>
{
    public Guid ProjectId { get; init; }
    
    /// <summary>
    /// ID of task
    /// </summary>
    public Guid TaskId { get; init; }
    
    /// <summary>
    /// On client, they will send "Title", "Description", ...
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<UpdateProjectTaskKey>))]
    public UpdateProjectTaskKey Key { get; init; }

    public string Value { get; init; } = null!;
}
