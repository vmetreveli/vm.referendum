using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using vm.referendum.Domain.Primitives;
using vm.referendum.Domain.Services;

namespace vm.referendum.Infrastructure.Services;

public sealed class EmailService(IOptions<EmailConfiguration> emailConfig, ILogger<EmailService> logger)
    : IEmailService
{
    private readonly EmailConfiguration _emailConfig = emailConfig.Value;
    private readonly ILogger<EmailService> _logger = logger;

    public async Task SendEmailAsync(SendEmailDto emailDto, CancellationToken cancellationToken)
    {
        MimeMessage email = new()
        {
            Subject = emailDto.Subject,
            To =
            {
                MailboxAddress.Parse(emailDto.To)
            },
            Body = new TextPart(TextFormat.Html)
            {
                Text = emailDto.Html
            },
            From =
            {
                MailboxAddress.Parse(_emailConfig.From)
            }
        };
        using SmtpClient smtp = new();
        await smtp.ConnectAsync(_emailConfig.Host, _emailConfig.Port, true,
            cancellationToken);
        smtp.AuthenticationMechanisms.Remove("XOAUTH2");

        await smtp.AuthenticateAsync(_emailConfig.From, _emailConfig.Password, cancellationToken);
        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}