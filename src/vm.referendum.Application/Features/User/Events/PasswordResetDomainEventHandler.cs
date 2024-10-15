using Framework.Abstractions.Events;
using Microsoft.Extensions.Logging;
using vm.referendum.Domain.Events.DomainEvents;
using vm.referendum.Domain.Services;

namespace vm.referendum.Application.Features.User.Events;

public class PasswordResetDomainEventHandler(
    ILogger<PasswordResetDomainEventHandler> logger,
    IEmailService emailService)
    : IEventHandler<PasswordResetDomainEvent>
{
    private readonly ILogger<PasswordResetDomainEventHandler> _logger = logger;


    public Task HandleAsync(PasswordResetDomainEvent @event, CancellationToken cancellationToken = default)
    {
        emailService.SendEmailAsync(@event.SendEmail, cancellationToken);
        return Task.CompletedTask;
    }
}