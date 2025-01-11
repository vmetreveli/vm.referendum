using vm.referendum.Domain.Primitives;

namespace vm.referendum.Domain.Services;

public interface IEmailService
{
    Task SendEmailAsync(SendEmailDto emailDto, CancellationToken cancellationToken = default);
}