
using JobNet.Data;
using JobNet.DTOs;
using JobNet.Exceptions;
using JobNet.Extensions;
using JobNet.Interfaces.Services;
using JobNet.Models.Entities;
using JobNet.Utilities;
using Microsoft.EntityFrameworkCore;

namespace JobNet.Services;
public class AdminsService : IAdminService
{
    private readonly string ADMIN_EMAIL_IS_ALREADY_REGISTERED = "This admin is already registered";
    private readonly string ADMIN_IS_NOT_EXIST = "This admin is not exist";
    private readonly JobNetDatabaseContext _databaseContext;
    public AdminsService(JobNetDatabaseContext jobNetDatabaseContext)
    {
        _databaseContext = jobNetDatabaseContext;
    }

    public async Task<Admin?> GetAdminById(int id)
    {
        try
        {
            return await _databaseContext.Admins.FindAsync(id);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Admin?> GetAdminByEmail(string email)
    {
        try
        {
            return await _databaseContext.Admins.FirstOrDefaultAsync(a => a.Email == email);

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeName(int adminId, string newName)
    {
        try
        {
            var admin = await _databaseContext.Admins.FindAsync(adminId) ?? throw new BadRequestException(ADMIN_IS_NOT_EXIST);
            admin.Name = newName;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeActiveStatus(int adminId, bool isActive)
    {
        try
        {
            var admin = await _databaseContext.Admins.FindAsync(adminId) ?? throw new BadRequestException(ADMIN_IS_NOT_EXIST);
            admin.IsActive = isActive;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeAdminPassword(int adminId, string newPassword)
    {
        try
        {
            var existence = await _databaseContext.Admins.FindAsync(adminId) ?? throw new BadRequestException(ADMIN_IS_NOT_EXIST);
            string hashedPassword = PasswordUtil.HashPassword(newPassword, out byte[] salt);
            existence.Password = hashedPassword;
            existence.PasswordSalt = salt;
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<AdminDTO> CreateNewAdmin(CreateAdminDTO user)
    {
        try
        {
            bool isExist = await _databaseContext.Admins.AnyAsync(e => user.Email == e.Email);
            if (isExist)
            {
                throw new BadRequestException(ADMIN_EMAIL_IS_ALREADY_REGISTERED);
            }
            Admin newAdmin = user.ToAdmin();
            await _databaseContext.Admins.AddAsync(newAdmin);
            await _databaseContext.SaveChangesAsync();
            return newAdmin.ToAdminDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
}