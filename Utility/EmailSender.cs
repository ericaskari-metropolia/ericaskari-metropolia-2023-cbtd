using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Utility;

public class EmailSender : IEmailSender
{
    public string SendGridSecret { get; set; }

    public EmailSender(IConfiguration _config)
    {
        SendGridSecret = Environment.GetEnvironmentVariable("APP_SENDGRID_APIKEY") ?? "";
        Console.WriteLine(SendGridSecret);
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine("Sending Email.....");
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("rich@richfry.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        var client = new SendGridClient(SendGridSecret);
        var from = new EmailAddress("asmohammad001@gmail.com", "Cheaper By the Dozen");
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
        return client.SendEmailAsync(msg);

    }
}

