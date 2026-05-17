namespace SASS.Chat.Domain.Events.Files;

public sealed record FileUploadedDomainEvent(Guid FileId) : DomainEvent;
