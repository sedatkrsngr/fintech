using System.Net.Mail;
using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Configuration;
using Fintech.NotificationService.Domain.Enums;
using Microsoft.Extensions.Options;

namespace Fintech.NotificationService.Infrastructure.Providers.Email;

public sealed class MailpitEmailProvider : IEmailProvider
{
    private readonly MailpitOptions _options;

    public MailpitEmailProvider(IOptions<MailpitOptions> options)
    {
        _options = options.Value;
    }

    public bool CanHandle(string providerKey, NotificationProviderType providerType)
    {
        return providerType == NotificationProviderType.Smtp
            && string.Equals(providerKey, "mailpit", StringComparison.OrdinalIgnoreCase);
    }

    public async Task SendAsync(
        Guid providerId,
        string toEmail,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        using var message = new MailMessage(_options.FromEmail, toEmail, subject, body);
        using var client = new SmtpClient(_options.Host, _options.Port);

        cancellationToken.ThrowIfCancellationRequested();
        await client.SendMailAsync(message, cancellationToken);
    }
}
