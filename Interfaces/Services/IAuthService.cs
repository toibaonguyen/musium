using JobNet.DTOs;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace JobNet.Interfaces.Services;

public interface IAuthService
{
    Task<AuthenticationTokenDTO> LoginAsUser(string email, string password);
    Task<AuthenticationTokenDTO> LoginAsAdmin(string email, string password);
    Task<AuthenticationTokenDTO> RefreshTokens(int userId, string role, string refreshToken);
    Task CreateNewAdmin(CreateAdminDTO user);
    Task ConfirmUser(int userId, string token);
    Task ResendVerificationEmail(string email);
    Task ChangeUserPassword(int userId, string newPassword);
    Task ChangeAdminPassword(int adminId, string newPassword);
    Task SendResetUserPasswordConfirmationEmail(string email);
    Task RegisterUser(CreateUserDTO user);
    Task ConfirmResetPassword(int userId, string token);
    // Task<AuthenticationResponse> ResendConfirmationEmail(ResendEmailConfirmationRequest requestBody);
    // Task<AuthenticationResponse> SendResetPasswordEmail(SendResetPasswordEmailRequest requestBody);
    // Task<AuthenticationResponse> DeleteUser(int UserId);
}