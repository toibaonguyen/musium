


using JobNet.DTOs;
using JobNet.Models.Entities;
namespace JobNet.Interfaces.Services;
public interface IAdminService
{
    Task<AdminDTO> CreateNewAdmin(CreateAdminDTO user);
    Task<Admin?> GetAdminById(int id);
    Task<Admin?> GetAdminByEmail(string email);
    Task ChangeName(int adminId, string newName);
    Task ChangeActiveStatus(int adminId, bool isActive);
    Task ChangeAdminPassword(int adminId, string newPassword);
}