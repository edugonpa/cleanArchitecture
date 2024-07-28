using System.Net;
using System.Net.Mail;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infrastructure.Email;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure;

internal sealed class EmailService : IEmailService
{
    public GmailSettings _gmailSettings { get;}
    public EmailService(IOptions<GmailSettings> gmailSettings)
    {
        _gmailSettings = gmailSettings.Value;
    }
    public void Send(string recipient, string subject, string body)
    {
        return;
        try
        {
            var fromEmail = _gmailSettings.Username;
            var password = _gmailSettings.Password;

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail!);
            message.Subject = subject;
            message.To.Add(new MailAddress(recipient));
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = _gmailSettings.Port,
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };
            
            smtpClient.Send(message);
        }
        catch (Exception e)
        {
            throw new Exception("No se pudo envíar el email", e);
        }
    }

}