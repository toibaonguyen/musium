using Microsoft.Extensions.Options;
using JobNet.Models;
using JobNet.Settings;
using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;
using JobNet.Models.Entities;
using JobNet.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using JobNet.Interfaces.Services;
using JobNet.Exceptions;
using JobNet.Utilities;


namespace JobNet.Services;

public class UsersService : IUserService
{

    private readonly JobNetDatabaseContext _databaseContext;
    public UsersService(JobNetDatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }
    public async Task CreateNewInactiveUser(CreateUserDTO user, bool isEmailConfirmed)
    {
        try
        {
            var existence = await _databaseContext.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
            if (existence != null)
            {
                //Throw exception because this user has been existed
                throw new BadRequestException("User is already register!");
            }
            await _databaseContext.Users.AddAsync(user.ToInactiveUser(isEmailConfirmed));
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserPassword(int userId, string newPassword)
    {
        try
        {
            var existence = await _databaseContext.Users.FindAsync(userId) ?? throw new BadRequestException("User is not exist");
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
    public async Task CreateNewActiveUser(CreateUserDTO user, bool isEmailConfirmed)
    {
        try
        {
            var existence = await _databaseContext.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
            if (existence != null)
            {
                //Throw exception because this user has been existed
                throw new BadRequestException("User is already register!");
            }
            await _databaseContext.Users.AddAsync(user.ToActiveUser(isEmailConfirmed));
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<User?> GetUserByEmail(string email)
    {
        try
        {
            var existence = await _databaseContext.Users.FirstOrDefaultAsync(e => e.Email == email);
            return existence;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<User?> GetUserById(int id)
    {
        try
        {
            var existence = await _databaseContext.Users.FindAsync(id);
            return existence;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO?> GetProfileUserById(int id)
    {
        try
        {
            var existence = await _databaseContext.Users.FindAsync(id);
            return existence?.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<IEnumerable<ListUserDTO>> GetListOfUser()
    {
        try
        {
            var users = await _databaseContext.Users.ToListAsync();
            var listUsers = new List<ListUserDTO>();
            foreach (User user in users)
            {
                listUsers.Add(user.ToListUserDTO());
            }
            return listUsers ?? ([]);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserAvatar(int userId, string newAvatar)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.Avatar, newAvatar));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserBackground(int userId, string newBackground)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.BackgroundImage, newBackground));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeUserName(int userId, string name)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.Name, name));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeSkills(int userId, IList<string> skills)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.Skills, skills));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeBirthday(int userId, DateTime birthday)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.Birthday, birthday));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeLocation(int userId, string location)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.Location, location));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeActiveStatus(int userId, bool isActive)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.IsActive, isActive));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task ChangeEmailConfirmationStatus(int userId, bool isEmailConfirmed)
    {
        try
        {
            await _databaseContext.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(setter => setter.SetProperty(u => u.IsEmailConfirmed, isEmailConfirmed));
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task DeleteUser(int userId)
    {
        try
        {
            User? user = await _databaseContext.Users.FindAsync(userId) ?? throw new BadRequestException("User is not exist");
            _databaseContext.Users.Remove(user);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}