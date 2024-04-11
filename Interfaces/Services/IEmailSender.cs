

using System.Net.Mail;

namespace JobNet.Interfaces.Services;

public interface IEmailSenderService
{
    Task SendEmailVerification(string toEmail, string verificationUrl);
    Task SendResetPasswordEmail(string toEmail, string newPassword);
    Task SendResetPasswordConfirmation(string toEmail, string confirmationUrl);
}
