// using Framework.Abstractions.Outbox;
// using Framework.Abstractions.Primitives;
// using MediatR;
// using Microsoft.EntityFrameworkCore.ChangeTracking;
// using Microsoft.EntityFrameworkCore.Diagnostics;
// using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;
// using Referendum.Domain.Primitives;
// using Referendum.Domain.Primitives.Events;
// using Referendum.Infrastructure.Clock;
// using Referendum.Infrastructure.Outbox;
//
// namespace Referendum.Infrastructure.Interceptors;
//
// public sealed class UpdateEntitiesInterceptor(
//     ILogger<UpdateEntitiesInterceptor> logger,
//     IDateTimeProvider dateTimeProvider)
//     : SaveChangesInterceptor
// {
//     private readonly ILogger<UpdateEntitiesInterceptor> _logger = logger;
//
//     private static readonly JsonSerializerSettings JsonSerializerSettings = new()
//     {
//         TypeNameHandling = TypeNameHandling.All
//     };
//
//     /// <summary>
//     ///     Updates the entities implementing <see>
//     ///         <cref>IAuditableEntity</cref>
//     ///     </see>
//     ///     interface.
//     /// </summary>
//     /// <param name="dbContext"></param>
//     /// <param name="utcNow"></param>
//     private static void UpdateAuditableEntities(DbContext dbContext, DateTime utcNow)
//     {
//         foreach (var entityEntry in dbContext.ChangeTracker.Entries<EntityBase<>>())
//         {
//             if (entityEntry.State == EntityState.Added)
//                 entityEntry.Property(nameof(EntityBase.CreatedOn)).CurrentValue = utcNow;
//
//             if (entityEntry.State == EntityState.Modified)
//                 entityEntry.Property(nameof(EntityBase.ModifiedOn)).CurrentValue = utcNow;
//         }
//     }
//
//
//     private static void UpdateDeletableEntities(DbContext dbContext, DateTime utcNow)
//     {
//         foreach (var entityEntry in dbContext.ChangeTracker.Entries<EntityBase>())
//         {
//             if (entityEntry.State != EntityState.Deleted) continue;
//
//             entityEntry.Property(nameof(EntityBase.DeletedOn)).CurrentValue = utcNow;
//
//             entityEntry.Property(nameof(EntityBase.IsDelete)).CurrentValue = true;
//
//             entityEntry.State = EntityState.Modified;
//
//             UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
//         }
//     }
//
//     private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
//     {
//         if (!entityEntry.References.Any()) return;
//
//         foreach (var referenceEntry in entityEntry.References.Where(r =>
//                      r.TargetEntry!.State == EntityState.Deleted))
//         {
//             referenceEntry.TargetEntry!.State = EntityState.Unchanged;
//
//             UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
//         }
//     }
//
//     public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
//         DbContextEventData eventData,
//         InterceptionResult<int> result,
//         CancellationToken cancellationToken = default)
//     {
//         var dbContext = eventData.Context;
//         if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);
//
//         var utcNow = DateTime.UtcNow;
//
//         UpdateAuditableEntities(dbContext, utcNow);
//         UpdateDeletableEntities(dbContext, utcNow);
//
//       //  AddDomainEventsAsOutboxMessages(dbContext);
//
//         return base.SavingChangesAsync(eventData, result, cancellationToken);
//     }
//
//     private void AddDomainEventsAsOutboxMessages(DbContext dbContext)
//     {
//         var outboxMessages = dbContext.ChangeTracker
//             .Entries<EntityBase>()
//             .Where(entry => entry.Entity.DomainEvents.Any())
//             .Select(entry => entry.Entity)
//             .SelectMany(entity =>
//             {
//                 var domainEvents = entity.DomainEvents;
//
//                 entity.ClearDomainEvents();
//
//                 return domainEvents;
//             })
//             .Select(domainEvent => new OutboxMessage(
//                 Guid.NewGuid(),
//                 dateTimeProvider.UtcNow,
//                 domainEvent.GetType().Name,
//                 JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
//             .ToList();
//         if (outboxMessages.Any())
//         {
//             dbContext.AddRange(outboxMessages);
//         }
//     }
// }

