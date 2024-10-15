using Framework.Abstractions.Events;
using Microsoft.Extensions.Logging;
using vm.referendum.Domain.Events.DomainEvents;
using vm.referendum.Domain.Primitives;
using vm.referendum.Domain.Services;

namespace vm.referendum.Application.Features.User.Events;

public class UserCreatedDomainEventHandler(ILogger<UserCreatedDomainEventHandler> logger, IEmailService emailService)
    : IEventHandler<UserCreatedDomainEvent>
{
    public Task HandleAsync(UserCreatedDomainEvent @event, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UserCreated User: {@User}", @event.User.Email);

        emailService.SendEmailAsync(new SendEmailDto
        {
            Subject = "Registration Verification",
            Html = "You are Welcome!!!",
            To = @event.User.Email
        }, cancellationToken);
        return Task.CompletedTask;
    }
}