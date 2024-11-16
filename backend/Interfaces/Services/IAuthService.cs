using JobNet.DTOs;
using JobNet.Models.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace JobNet.Interfaces.Services;

public interface IAuthService
{
    Task<AuthenticationResponse> LoginAsUser(string email, string password);
    Task<AuthenticationResponse> LoginAsAdmin(string email, string password);
    Task<AuthenticationResponse> RefreshTokens(int userId, string role, string refreshToken);
    Task CreateNewAdmin(CreateAdminDTO user);
    Task ConfirmUser(int userId, string token);
    Task ResendVerificationEmail(string email);
    Task ChangeUserPassword(int userId, string newPassword);
    Task ChangeAdminPassword(int adminId, string newPassword);
    Task SendResetUserPasswordConfirmationEmail(string email);
    Task RegisterUser(CreateUserDTO user);
    Task ConfirmResetPassword(int userId, string token);
    Task Logout(string userRole, int userId, string? notificationRegistrationToken);
}