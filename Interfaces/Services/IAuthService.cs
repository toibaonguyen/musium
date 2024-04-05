using JobNet.DTOs;
using JobNet.Models.Core.Responses;

namespace JobNet.Interfaces.Services;

public interface IAuthService
{
    Task<AuthenticationResponse> LoginAsUser(string email, string password);
    Task<AuthenticationResponse> LoginAsAdmin(string email, string password);
    Task<AuthenticationResponse> RefreshTokens(int userId, string role, string refreshToken);
    // Task<AuthenticationResponse> ConfirmUser(ConfirmUserRequest requestBody);
    // Task<AuthenticationResponse> ResendConfirmationEmail(ResendEmailConfirmationRequest requestBody, string confirmUserEndpointUrl);
    // Task<AuthenticationResponse> ResetPassword(ResetPasswordRequest requestBody);
    // Task<AuthenticationResponse> SendResetPasswordEmail(SendResetPasswordEmailRequest requestBody, string resetPasswordEndpointUrl);
    // Task<AuthenticationResponse> RegisterUser(CreateUserDTO user);
    // Task<AuthenticationResponse> ResendConfirmationEmail(ResendEmailConfirmationRequest requestBody);
    // Task<AuthenticationResponse> SendResetPasswordEmail(SendResetPasswordEmailRequest requestBody);
    // Task<AuthenticationResponse> DeleteUser(int UserId);
}