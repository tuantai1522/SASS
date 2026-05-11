namespace SASS.Chat.Domain.Events.Conversations;

public sealed record MessageCreatedDomainEvent(Guid MessageId) : DomainEvent;
