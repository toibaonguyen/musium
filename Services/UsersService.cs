using Microsoft.Extensions.Options;
using JobNet.Models;
using JobNet.Settings;
using JobNet.Data;
using Microsoft.EntityFrameworkCore;
using JobNet.DTOs;
using JobNet.Models.Entities;
using JobNet.Extensions;


namespace JobNet.Services;

public class UsersService
{
    private readonly JobNetDatabaseContext _databaseContext;
    public UsersService(JobNetDatabaseContext databaseContext)
    {
        this._databaseContext = databaseContext;
    }
    public async Task<int> TestCreateNewUser(CreateUserDTO newUser)
    {

        // _ = await _databaseContext.AddAsync<User>(newUser.ToActiveUser("", ""));
        // // await _databaseContext.SaveChangesAsync();
        try
        {
            _databaseContext.Users.Add(new()
            {
                Name = "",
                Avatar = "",
                BackgroundImage = "",
                Email = "",
                Password = "",
                Location = "",
                Birthday = (new DateTime(2024, 1, 1)).ToUniversalTime(),
                Experiences = [],
                Certifications = [],
                Educations = [],
                JobNetGroups = [],
                Skills = [],
                IsActive = true
            });
            await _databaseContext.SaveChangesAsync();
            return 5;
        }
        catch (Exception)
        {
            Console.WriteLine("Loi nua roi dume");
            throw;
        }


    }
    public async Task<User?> testGetUserWithId1()
    {

        // _ = await _databaseContext.AddAsync<User>(newUser.ToActiveUser("", ""));
        // // await _databaseContext.SaveChangesAsync();
        try
        {
            var user = await _databaseContext.Users.FirstOrDefaultAsync(user => user.Id == 1);
            return user;
        }
        catch (Exception)
        {
            Console.WriteLine("Loi nua roi dume");
            throw;
        }


    }
    // public async Task<UserDTO> GetUserByID(string id)
    // {

    // }
    // public async Task<UserDTO> UpdatePatchUser(string id)
    // {

    // }
    // public async Task DisableUser(string id)
    // {

    // }
}