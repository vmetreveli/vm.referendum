using Meadow_Framework.Core.Abstractions.Events;
using vm.referendum.Domain.Entities.User;

namespace vm.referendum.Domain.Events.DomainEvents;

/// <summary>
///     Represents the event that is raised when a user is created.
/// </summary>
public sealed class UserNameChangedDomainEvent(User user) : IDomainEvent
{
    public User User { get; } = user;
}