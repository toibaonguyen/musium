


using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using JobNet.Interfaces.Services;
using JobNet.Settings;
using Microsoft.Extensions.Options;

namespace JobNet.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly MailSetting _setting;
    public EmailSenderService(IOptions<MailSetting> options)
    {
        _setting = options.Value;
    }


    private async Task SendEmail(string toEmail, string subject, string message)
    {
        try
        {
            SmtpClient client = new SmtpClient(_setting.Host, _setting.Port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(_setting.Email, _setting.Password),
            };
            await client.SendMailAsync(_setting.Email, toEmail, subject, message);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendEmailVerification(string toEmail, string verificationUrl)
    {
        try
        {

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendResetPasswordConfirmation(string toEmail, string confirmationUrl)
    {
        try
        {

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task SendResetPasswordEmail(string toEmail, string newPassword)
    {
        try
        {
            var message = $"This is your new password: {newPassword}";
            await this.SendEmail(toEmail, "Reset password", message);
        }
        catch (Exception)
        {
            throw;
        }
    }
}