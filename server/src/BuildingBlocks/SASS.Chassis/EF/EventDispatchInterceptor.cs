using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SASS.SharedKernel.EventPublishers;
using SASS.SharedKernel.SeedWork;

namespace SASS.Chassis.EF;

/// <summary>
/// To publish domain events when saving changes in database
/// </summary>
/// <param name="dispatcher">Interface used for publishing event service</param>
internal sealed class EventDispatchInterceptor(IDomainEventDispatcher dispatcher) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        var ctx = eventData.Context;

        if (ctx is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var entitiesWithEvents = ctx
            .ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(x => x.DomainEvents.Count != 0)
            .ToImmutableList();

        await dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
