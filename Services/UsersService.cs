using Microsoft.Extensions.Options;
using JobNet.Models;
using JobNet.Settings;
using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;
using JobNet.Models.Entities;
using JobNet.Extensions;
using Microsoft.AspNetCore.JsonPatch;


namespace JobNet.Services;

public class UsersService
{
    private readonly JobNetDatabaseContext _databaseContext;
    public UsersService(JobNetDatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }
    public async Task CreateNewUser(CreateUserDTO user, string avatar, string backgroundImage)
    {
        try
        {
            await _databaseContext.Users.AddAsync(user.ToActiveUser(avatar, backgroundImage));
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ProfileUserDTO?> GetProfileUserDTO(int id)
    {
        try
        {
            User? user = await _databaseContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            return user.ToProfileUserDTO();
        }
        catch (Exception)
        {
            throw;
        }
    }
}