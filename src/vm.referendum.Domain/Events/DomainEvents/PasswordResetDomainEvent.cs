using Framework.Abstractions.Events;
using vm.referendum.Domain.Primitives;

namespace vm.referendum.Domain.Events.DomainEvents;

/// <summary>
///     Represents the event that is raised when a user is created.
/// </summary>
public sealed class PasswordResetDomainEvent(SendEmailDto sendEmail) : IDomainEvent
{
    public SendEmailDto SendEmail { get; set; } = sendEmail;
}