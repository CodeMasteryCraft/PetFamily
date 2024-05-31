using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PetFamily.Application.Providers;
using PetFamily.Infrastructure.Options;

namespace PetFamily.Infrastructure.Providers;

public class MailProvider : IMailProvider
{
    private readonly MailOptions _mailOptions;

    public MailProvider(IOptions<MailOptions> options)
    {
        _mailOptions = options.Value;
    }

    public async Task SendMessage(string message, Guid userId)
    {
        var mail = InitializeMail(message);

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_mailOptions.Host, _mailOptions.Port, _mailOptions.UseSSL);
            await client.AuthenticateAsync(_mailOptions.UserName, _mailOptions.Password);
            await client.SendAsync(mail);

            await client.DisconnectAsync(true);
        }
    }

    private MimeMessage InitializeMail(string message)
    {
        var mail = new MimeMessage();

        mail.From.Add(new MailboxAddress(_mailOptions.DisplayName, _mailOptions.From));
        mail.To.Add(new MailboxAddress("", "sachkovkrl@gmail.com"));

        mail.Subject = "Заголовок";
        mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        return mail;
    }
}