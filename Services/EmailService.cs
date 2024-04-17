using System.Security.Policy;
using JobNet.Interfaces.Services;
using JobNet.Settings;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace JobNet.Services;

public class EmailSenderService : IEmailSenderService, IEmailSender
{
    private readonly EmailSenderProviderSetting _setting;
    private readonly ILogger<EmailSenderService> _logger;
    public EmailSenderService(ILogger<EmailSenderService> logger, IOptions<EmailSenderProviderSetting> options)
    {
        _setting = options.Value;
        _logger = logger;
    }


    public async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {

        try
        {
            using (MimeMessage emailMessage = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress(_setting.SenderName, _setting.SenderEmail);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(toEmail, toEmail);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = subject;
                BodyBuilder emailBodyBuilder = new BodyBuilder
                {
                    HtmlBody = htmlMessage
                };
                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                using (SmtpClient mailClient = new SmtpClient())
                {
                    await mailClient.ConnectAsync(_setting.Server, _setting.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    await mailClient.AuthenticateAsync(_setting.UserName, _setting.Password);
                    await mailClient.SendAsync(emailMessage);
                    await mailClient.DisconnectAsync(true);
                }
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendEmailVerificationAsync(string toEmail, string verificationUrl)
    {
        try
        {
            var message = $"<div><strong>Please click to this url to verify account:</strong> {verificationUrl}</div>";
            await this.SendEmailAsync(toEmail, "Email verification", message);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendResetPasswordConfirmationAsync(string toEmail, string confirmationUrl)
    {
        try
        {
            var message = $"Please click to this url to confirm reset password request: {confirmationUrl}";
            await this.SendEmailAsync(toEmail, "Reset password confirmation", message);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendNewResetPasswordEmailAsync(string toEmail, string newPassword)
    {
        try
        {
            var message = $"This is your new password: {newPassword}";
            await this.SendEmailAsync(toEmail, "New password", message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}