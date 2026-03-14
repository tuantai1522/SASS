using System.Collections.Immutable;
using MediatR;
using SASS.SharedKernel.SeedWork;

namespace SASS.SharedKernel.EventPublishers;

/// <summary>
/// This handler will be responsible for publishing events when saving changes successfully in db using Mediator
/// </summary>
/// <param name="publisher">One event can be subscribed by multiple consumers</param>
public sealed class MediatorDomainEventDispatcher(IPublisher publisher) : IDomainEventDispatcher
{
    public async Task DispatchAndClearEvents(ImmutableList<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            if (entity is not HasDomainEvents hasDomainEvents)
            {
                continue;
            }

            DomainEvent[] events = [.. hasDomainEvents.DomainEvents];
            hasDomainEvents.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await publisher.Publish(domainEvent);
            }
        }
    }
}
