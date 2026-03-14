using System.Collections.Immutable;
using SASS.SharedKernel.SeedWork;

namespace SASS.SharedKernel.EventPublishers;

/// <summary>
/// Interface used for publish event when saving changes in interceptors
/// This can use Mediator or RabbitMQ
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(ImmutableList<IHasDomainEvents> entitiesWithEvents);
}
