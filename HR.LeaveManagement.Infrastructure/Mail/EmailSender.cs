using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LeaveManagement.Infrastructure.Mail;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings emailSettings;

    public EmailSender(IOptions<EmailSettings> options)
    {
        emailSettings = options.Value;
    }
    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(emailSettings.ApiKey);
        var to = new EmailAddress((string)email.To);
        var from = new EmailAddress
        {
            Email = emailSettings.FromAddress,
            Name = emailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(from, to, (string)email.Subject, (string)email.Body, (string)email.Body);
        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
