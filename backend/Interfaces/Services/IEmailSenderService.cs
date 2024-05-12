

using System.Net.Mail;

namespace JobNet.Interfaces.Services;

public interface IEmailSenderService
{
    Task SendEmailVerificationAsync(string toEmail, string verificationUrl);
    Task SendNewResetPasswordEmailAsync(string toEmail, string newPassword);
    Task SendResetPasswordConfirmationAsync(string toEmail, string confirmationUrl);
}
