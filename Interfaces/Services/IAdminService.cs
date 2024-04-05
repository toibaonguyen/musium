


using JobNet.DTOs;
using JobNet.Models.Entities;
namespace JobNet.Interfaces.Services;
public interface IAdminService
{
    Task CreateNewActiveAdmin(CreateAdminDTO user);
    Task CreateNewInactiveAdmin(CreateAdminDTO user);
    Task<Admin> GetAdminById(int id);
    Task<Admin> GetAdminByEmail(string email);
    Task ChangeName(int adminId, string newName);
    Task ChangeActiveStatus(int adminId, bool isActive);
}