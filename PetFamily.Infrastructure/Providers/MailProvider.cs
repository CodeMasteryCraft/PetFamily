using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using PetFamily.Application.Messages;
using PetFamily.Application.Providers;
using PetFamily.Domain.Common;
using PetFamily.Domain.ValueObjects;
using PetFamily.Infrastructure.Options;

namespace PetFamily.Infrastructure.Providers;

public class MailProvider : IMailProvider
{
    private readonly ILogger<MailProvider> _logger;
    private readonly MailOptions _mailOptions;

    public MailProvider(IOptions<MailOptions> options, ILogger<MailProvider> logger)
    {
        _logger = logger;
        _mailOptions = options.Value;
    }

    public async Task SendMessage(EmailNotification emailNotification)
    {
        try
        {
            var mail = InitializeMail(emailNotification.Message, emailNotification.Email);

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_mailOptions.Host, _mailOptions.Port, _mailOptions.UseSSL);
                await client.AuthenticateAsync(_mailOptions.UserName, _mailOptions.Password);
                await client.SendAsync(mail);

                _logger.LogInformation("Email send to {email}", emailNotification.Email.Value);

                await client.DisconnectAsync(true);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Error when try to send email: {e}", e.Message);
        }
    }

    private MimeMessage InitializeMail(string message, Email email)
    {
        var mail = new MimeMessage();

        mail.From.Add(new MailboxAddress(_mailOptions.DisplayName, _mailOptions.From));
        mail.To.Add(new MailboxAddress("", email.Value));

        mail.Subject = "Заголовок";
        mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        return mail;
    }
}